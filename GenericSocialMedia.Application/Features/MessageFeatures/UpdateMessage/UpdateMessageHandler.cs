using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Hubs;
using GenericSocialMedia.Application.Repositories;
using Microsoft.AspNetCore.SignalR;

namespace GenericSocialMedia.Application.Features.MessageFeatures.UpdateMessage
{
    public class UpdateMessageHandler : IRequestHandler<UpdateMessageRequest, UpdateMessageResponse>
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public UpdateMessageHandler(IMessageRepository messageRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UpdateMessageResponse> Handle(UpdateMessageRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);
            var message = await _messageRepository.GetWithUsers(request.Id, cancellationToken);
            var receiver = message.Receiver;
            message.Content = request.Message;
            _messageRepository.Update(message);
            await _unitOfWork.Save(cancellationToken);
            await _hubContext.Clients.All.SendAsync(receiver.ChatSecret.ToString(), currUser.Username, message.Content, message.Id);
            return _mapper.Map<UpdateMessageResponse>(message);
        }
    }
}
