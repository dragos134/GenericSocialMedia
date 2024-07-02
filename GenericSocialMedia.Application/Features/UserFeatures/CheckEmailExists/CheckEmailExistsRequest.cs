using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.CheckEmailExists
{
    public sealed record CheckEmailExistsRequest(string SearchText) : IRequest<CheckEmailExistsResponse>;
}
