using MediatR;

namespace GenericSocialMedia.Application.Features.PostFeatures.GetAllPosts
{
    public sealed record GetAllPostsRequest : IRequest<List<GetAllPostsResponse>>;
}
