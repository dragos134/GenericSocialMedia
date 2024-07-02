using MediatR;
using Microsoft.AspNetCore.Http;

namespace GenericSocialMedia.Application.Features.PostFeatures.UpdatePost
{
    public sealed record UpdatePostRequest(int PostId, int UserId, IFormFile? Photo, string? Description) : IRequest<UpdatePostResponse>;
}
