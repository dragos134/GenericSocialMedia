

namespace GenericSocialMedia.Application.Features.PostFeatures.GetAllPosts
{
    public sealed record GetAllPostsResponse
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string UserFullName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageId { get; set; } = string.Empty;
        public string? ProfilePictureId { get; set; }
        public DateTime PostTime { get; set; }
    }
}
