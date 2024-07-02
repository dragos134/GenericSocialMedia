using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.SearchUsers
{
    public class SearchUsersHandler : IRequestHandler<SearchUsersRequest, List<SearchUsersResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public SearchUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<SearchUsersResponse>> Handle(SearchUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.SearchUsers(request.SearchText.ToLower(), cancellationToken);
            return _mapper.Map<List<SearchUsersResponse>>(users);
        }
    }
}
