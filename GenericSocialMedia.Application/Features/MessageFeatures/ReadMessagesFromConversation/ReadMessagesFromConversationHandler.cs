using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.MessageFeatures.ReadMessagesFromConversation
{
    public class ReadMessagesFromConversationHandler(IMessageRepository messageRepository,
                                                     IUserRepository userRepository,
                                                     IUnitOfWork unitOfWork,
                                                     IMapper mapper) : IRequestHandler<ReadMessagesFromConversationRequest, ReadMessagesFromConversationResponse>
    {
        private readonly IMessageRepository _messageRepository = messageRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        public async Task<ReadMessagesFromConversationResponse> Handle(ReadMessagesFromConversationRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);
            var senderUser = await _userRepository.GetByUsername(request.SenderUsername, cancellationToken);
            var messagesToUpdate = await _messageRepository.GetUnreadMessagesFromConversation(senderUser.Id, currUser.Id, cancellationToken);
            messagesToUpdate.ForEach(msg =>
            {
                msg.IsRead = true;
                _messageRepository.Update(msg);
            });
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<ReadMessagesFromConversationResponse>(null);
        }
    }
}
