using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserRequest(string Email) : IRequest<CreateUserResponse>;
}
