using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetChatUsers
{
    public class GetChatUsersHandler : IRequestHandler<GetChatUsersRequest, List<GetChatUsersResponse>>
    {
        IUserRepository _userRepository;
        IMessageRepository _messageRepository;
        IMapper _mapper;
        public GetChatUsersHandler(IMapper mapper, IUserRepository userRepository, IMessageRepository messageRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _messageRepository = messageRepository;

        }
        public async Task<List<GetChatUsersResponse>> Handle(GetChatUsersRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);
            var chatUsers = await _userRepository.GetChatUsers(currUser.Id, cancellationToken);

            return _mapper.Map<List<GetChatUsersResponse>>(chatUsers);
        }
    }
}
