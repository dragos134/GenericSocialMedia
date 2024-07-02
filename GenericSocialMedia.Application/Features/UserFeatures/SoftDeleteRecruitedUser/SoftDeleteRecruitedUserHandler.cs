using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.SoftDeleteRecruitedUser
{
    public class SoftDeleteRecruitedUserHandler : IRequestHandler<SoftDeleteRecruitedUserRequest, SoftDeleteRecruitedUserResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SoftDeleteRecruitedUserHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<SoftDeleteRecruitedUserResponse> Handle(SoftDeleteRecruitedUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.UserId, cancellationToken);
            user.IsDeleted = true;
            _userRepository.Update(user);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<SoftDeleteRecruitedUserResponse>(null);
        }
    }
}
