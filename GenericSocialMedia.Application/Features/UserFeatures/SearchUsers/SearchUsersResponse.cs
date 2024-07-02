namespace GenericSocialMedia.Application.Features.UserFeatures.SearchUsers
{
    public sealed record SearchUsersResponse
    {
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? PhotoId { get; set; }
        public string LastMessage { get; set; } = string.Empty;
    }
}
