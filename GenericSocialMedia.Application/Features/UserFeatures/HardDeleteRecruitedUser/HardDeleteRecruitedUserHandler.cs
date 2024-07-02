using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.HardDeleteRecruitedUser
{
    public class HardDeleteRecruitedUserHandler : IRequestHandler<HardDeleteRecruitedUserRequest, HardDeleteRecruitedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public HardDeleteRecruitedUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<HardDeleteRecruitedUserResponse> Handle(HardDeleteRecruitedUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId, cancellationToken);
            _userRepository.Delete(user);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<HardDeleteRecruitedUserResponse>(null);
        }
    }
}
