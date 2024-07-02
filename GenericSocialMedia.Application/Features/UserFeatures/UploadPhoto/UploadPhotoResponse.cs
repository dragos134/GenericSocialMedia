namespace GenericSocialMedia.Application.Features.UserFeatures.UploadPhoto
{
    public sealed record UploadPhotoResponse
    {
        public string PhotoUrl { get; set; } = string.Empty;
    }
}
