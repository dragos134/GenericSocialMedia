using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.MessageFeatures.GetConversation
{
    public sealed class GetConversationHandler : IRequestHandler<GetConversationRequest, List<GetConversationResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetConversationHandler(IUnitOfWork unitOfWork, IMessageRepository messageRepository, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<GetConversationResponse>> Handle(GetConversationRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);
            var conversationUser = await _userRepository.GetByUsername(request.Username, cancellationToken);
            var conversation = await _messageRepository.GetConversation(currUser.Id, conversationUser.Id);
            return _mapper.Map<List<GetConversationResponse>>(conversation);
        }

    }
}
