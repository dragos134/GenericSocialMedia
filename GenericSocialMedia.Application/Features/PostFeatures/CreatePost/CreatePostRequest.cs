using MediatR;
using Microsoft.AspNetCore.Http;

namespace GenericSocialMedia.Application.Features.PostFeatures.CreatePost
{
    public sealed record CreatePostRequest(IFormFile Photo, string Description) : IRequest<CreatePostResponse>;
}
