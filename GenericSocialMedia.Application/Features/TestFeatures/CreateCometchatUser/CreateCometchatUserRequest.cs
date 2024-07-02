using MediatR;

namespace GenericSocialMedia.Application.Features.TestFeatures.CreateCometchatUser
{
    public sealed record CreateCometchatUserRequest(string Username, string Uid) : IRequest<CreateCometchatUserResponse>;
}
