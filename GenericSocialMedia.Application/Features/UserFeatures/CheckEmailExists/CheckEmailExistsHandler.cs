using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.CheckEmailExists
{
    public class CheckEmailExistsHandler : IRequestHandler<CheckEmailExistsRequest, CheckEmailExistsResponse>
    {
        private readonly IUserRepository _userRepository;
        public CheckEmailExistsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<CheckEmailExistsResponse> Handle(CheckEmailExistsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.SearchText, cancellationToken);
            if (user == null)
            {
                return new CheckEmailExistsResponse
                {
                    EmailExists = false
                };
            }
            return new CheckEmailExistsResponse { EmailExists = true };
        }
    }
}
