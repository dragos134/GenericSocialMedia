using MediatR;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetUserPosts
{
    public sealed record GetUserPostsRequest(string Username) : IRequest<IEnumerable<GetUserPostsResponse>>;
}
