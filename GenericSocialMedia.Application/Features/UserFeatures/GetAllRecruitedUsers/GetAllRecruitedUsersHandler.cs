using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers
{
    public class GetAllRecruitedUsersHandler : IRequestHandler<GetAllRecruitedUsersRequest, List<GetAllRecruitedUsersResponse>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetAllRecruitedUsersHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllRecruitedUsersResponse>> Handle(GetAllRecruitedUsersRequest request, CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllUsers(cancellationToken);

            return _mapper.Map<List<GetAllRecruitedUsersResponse>>(users);
        }
    }
}
