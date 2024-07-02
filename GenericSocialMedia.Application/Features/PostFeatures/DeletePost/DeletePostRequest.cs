using MediatR;

namespace GenericSocialMedia.Application.Features.PostFeatures.DeletePost
{
    public sealed record DeletePostRequest(int PostId, int UserId) : IRequest<DeletePostResponse>;
}
