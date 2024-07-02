using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetUserByUsername
{
    public sealed record GetUserByUsernameRequest(string Username) : IRequest<GetUserByUsernameResponse>;
}
