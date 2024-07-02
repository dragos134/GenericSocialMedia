using MediatR;
using Microsoft.AspNetCore.Http;

namespace GenericSocialMedia.Application.Features.UserFeatures.UploadPhoto
{
    public sealed record UploadPhotoRequest(IFormFile File) : IRequest<UploadPhotoResponse>;
}
