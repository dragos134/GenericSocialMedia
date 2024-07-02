using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetPaginatedChatUsers
{
    public class GetPaginatedChatUsersHandler : IRequestHandler<GetPaginatedChatUsersRequest, List<GetPaginatedChatUsersResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IChatUsersRepository _chatUsersRepository;
        private readonly IMapper _mapper;
        public GetPaginatedChatUsersHandler(IMapper mapper, IUserRepository userRepository, IMessageRepository messageRepository, IChatUsersRepository chatUsersRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _messageRepository = messageRepository;
            _chatUsersRepository = chatUsersRepository;
        }
        public async Task<List<GetPaginatedChatUsersResponse>> Handle(GetPaginatedChatUsersRequest request, CancellationToken cancellationToken)
        {
            var currUser = await _userRepository.GetCurrentUser(cancellationToken);

            var chatUsers = (await _chatUsersRepository.GetPaginatedChatUsersOfUser(currUser.Id, request.Skip, request.Take)).Select(x => x.ChatUser).ToList();


            return _mapper.Map<List<GetPaginatedChatUsersResponse>>(chatUsers);
        }
    }
}
