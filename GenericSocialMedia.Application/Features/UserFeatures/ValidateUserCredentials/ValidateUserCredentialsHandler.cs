using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Common.Helpers;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.AuthenticateUser
{
    public class ValidateUserCredentialsHandler : IRequestHandler<ValidateUserCredentialsRequest, ValidateUserCredentialsResponse?>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ValidateUserCredentialsHandler(IUnitOfWork unitOfWork, IUserRepository userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<ValidateUserCredentialsResponse?> Handle(ValidateUserCredentialsRequest request, CancellationToken cancellationToken)
        {
            var hashedPassword = AuthenticationHelpers.ComputeSha256Hash(request.Password);
            var user = await _userRepository.GetByCredentials(request.Email, hashedPassword, cancellationToken);
            if (user == null)
            {
                return null;
            }

            return _mapper.Map<ValidateUserCredentialsResponse>(user);
        }
    }
}
