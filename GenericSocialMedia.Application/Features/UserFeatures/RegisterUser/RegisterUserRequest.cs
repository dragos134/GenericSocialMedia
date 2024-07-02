using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.RegisterUser
{
    public sealed record RegisterUserRequest(
        string Email,
        string FirstName,
        string LastName,
        string Password,
        string Username,
        string City,
        string Gender,
        bool TCAccepted) : IRequest<RegisterUserResponse>;
}
