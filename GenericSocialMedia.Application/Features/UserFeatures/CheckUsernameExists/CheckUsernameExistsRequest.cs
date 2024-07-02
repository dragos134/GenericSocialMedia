using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.CheckUsernameExists
{
    public sealed record CheckUsernameExistsRequest(string SearchText) : IRequest<CheckUsernameExistsResponse>;
}
