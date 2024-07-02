using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.SearchUsers
{
    public sealed record SearchUsersRequest(string SearchText) : IRequest<List<SearchUsersResponse>>;
}
