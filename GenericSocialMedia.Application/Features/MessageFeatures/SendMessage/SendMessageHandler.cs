using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Hubs;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.SendMessage
{
    public sealed class SendMessageHandler : IRequestHandler<SendMessageRequest, SendMessageResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IChatUsersRepository _chatUsersRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public SendMessageHandler(IUnitOfWork unitOfWork, IMessageRepository messageRepository, IMapper mapper, IHubContext<ChatHub> hubContext, IUserRepository userRepository, IChatUsersRepository chatUsersRepository)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
            _mapper = mapper;
            _hubContext = hubContext;
            _userRepository = userRepository;
            _chatUsersRepository = chatUsersRepository;
        }

        public async Task<SendMessageResponse> Handle(SendMessageRequest request, CancellationToken cancellationToken)
        {
            // get sender and receiver users
            var receiverUser = await _userRepository.GetByUsername(request.Username, cancellationToken);
            var senderUser = await _userRepository.GetCurrentUser(cancellationToken);
            var chatSecret = receiverUser.ChatSecret.ToString();

            // check if the sender has any available messages and updating it accordingly
            if (senderUser.RemainingMessages != null)
            {
                if (senderUser.RemainingMessages <= 0)
                {
                    throw new InvalidOperationException();
                }
                else
                {
                    senderUser.RemainingMessages--;
                    _userRepository.Update(senderUser);
                }
            }

            // adding the new message in the DB
            var messageToSend = new Message
            {
                Sender = senderUser,
                Receiver = receiverUser,
                Content = request.Content,
            };
            _messageRepository.Create(messageToSend);

            // checking if there is already a conversation between these 2 users
            var chatUsers = await _chatUsersRepository.GetByUserIds(senderUser.Id, receiverUser.Id);
            var chatUser = await _chatUsersRepository.GetByUserIds(receiverUser.Id, senderUser.Id);
            if (chatUsers == null)
            {
                _chatUsersRepository.Create(new Domain.Entities.ChatUsers
                {
                    UserId = senderUser.Id,
                    ChatUserId = receiverUser.Id,
                    LastMessageAt = DateTime.Now,
                });

                _chatUsersRepository.Create(new Domain.Entities.ChatUsers
                {
                    UserId = receiverUser.Id,
                    ChatUserId = senderUser.Id,
                    LastMessageAt = DateTime.Now,
                });
            }
            else
            {
                chatUsers.LastMessageAt = DateTime.Now;
                chatUser.LastMessageAt = DateTime.Now;
                _chatUsersRepository.Update(chatUsers);
                _chatUsersRepository.Update(chatUser);
            }

            await _unitOfWork.Save(cancellationToken);

            // sending the message to the UI chat in real time using the hub context
            await _hubContext.Clients.All.SendAsync(chatSecret, senderUser.Username, messageToSend.Content, messageToSend.Id);
            await _hubContext.Clients.All.SendAsync($"{chatSecret}-notification", senderUser.Username, "asd", "asd");

            return _mapper.Map<SendMessageResponse>(messageToSend);
        }
    }
}
