using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Hubs;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.SendSpamMessage
{
    public class SendSpamMessageHandler : IRequestHandler<SendSpamMessageRequest, SendSpamMessageResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IChatUsersRepository _chatUsersRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        public SendSpamMessageHandler(IUserRepository userRepository, IMessageRepository messageRepository, IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext, IChatUsersRepository chatUsersRepository)
        {
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _chatUsersRepository = chatUsersRepository;
            _hubContext = hubContext;
        }

        public async Task<SendSpamMessageResponse> Handle(SendSpamMessageRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);
            var idsList = request.IdsList.Split(',');
            var messages = idsList.Select(id => new Message
            {
                Content = request.Content,
                SenderId = currUser.Id,
                ReceiverId = int.Parse(id),
                CreatedAt = DateTimeOffset.UtcNow,
                CreatedBy = currUser.Username
            });

            foreach (var message in messages)
            {
                _messageRepository.Create(message);

                // checking if there is already a conversation between these 2 users
                var chatUsers = await _chatUsersRepository.GetByUserIds(message.SenderId, message.ReceiverId);
                var chatUser = await _chatUsersRepository.GetByUserIds(message.ReceiverId, message.SenderId);
                if (chatUsers == null)
                {
                    _chatUsersRepository.Create(new GenericSocialMedia.Domain.Entities.ChatUsers
                    {
                        UserId = message.SenderId,
                        ChatUserId = message.ReceiverId,
                        LastMessageAt = DateTime.UtcNow,
                    });

                    _chatUsersRepository.Create(new GenericSocialMedia.Domain.Entities.ChatUsers
                    {
                        UserId = message.ReceiverId,
                        ChatUserId = message.SenderId,
                        LastMessageAt = DateTime.UtcNow,
                    });
                }
                else
                {
                    chatUsers.LastMessageAt = DateTime.UtcNow;
                    chatUser.LastMessageAt = DateTime.UtcNow;
                    _chatUsersRepository.Update(chatUsers);
                    _chatUsersRepository.Update(chatUser);
                }

                await _unitOfWork.Save(cancellationToken);
            }
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<SendSpamMessageResponse>(null);
        }
    }
}
