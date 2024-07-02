using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRegisteredUsers
{
    public class GetAllRegisteredUsersHandler : IRequestHandler<GetAllRegisteredUsersRequest, List<GetAllRegisteredUsersResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public GetAllRegisteredUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllRegisteredUsersResponse>> Handle(GetAllRegisteredUsersRequest request, CancellationToken cancellationToken)
        {
            var registeredUsers = await _userRepository.GetAllRegisteredUsers(cancellationToken);
            return _mapper.Map<List<GetAllRegisteredUsersResponse>>(registeredUsers);
        }
    }
}
