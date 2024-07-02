using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.UserFeatures.CheckUsernameExists
{
    public sealed class CheckUsernameExistsHandler : IRequestHandler<CheckUsernameExistsRequest, CheckUsernameExistsResponse>
    {
        private readonly IUserRepository _userRepository;
        public CheckUsernameExistsHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<CheckUsernameExistsResponse> Handle(CheckUsernameExistsRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByUsername(request.SearchText, cancellationToken);
            if (user == null)
            {
                return new CheckUsernameExistsResponse
                {
                    UsernameExists = false
                };
            }

            return new CheckUsernameExistsResponse { UsernameExists = true };
        }
    }
}
