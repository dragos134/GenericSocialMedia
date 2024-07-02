namespace GenericSocialMedia.Application.Features.UserFeatures.GetUserByUsername
{
    public sealed record GetUserByUsernameResponse
    {
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? PhotoId { get; set; }
        public string City { get; set; } = string.Empty;
        public string Subscription { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
    }
}
