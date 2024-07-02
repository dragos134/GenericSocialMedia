using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.AuthenticateUser
{
    public sealed record ValidateUserCredentialsRequest(string Email, string Password) : IRequest<ValidateUserCredentialsResponse>;
}
