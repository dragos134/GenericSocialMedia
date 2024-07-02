namespace GenericSocialMedia.Application.Features.PostFeatures.GetUserPosts
{
    public sealed record GetUserPostsResponse
    {
        public string ImageId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
