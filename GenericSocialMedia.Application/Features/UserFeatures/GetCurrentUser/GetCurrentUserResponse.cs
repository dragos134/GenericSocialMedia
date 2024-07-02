

namespace GenericSocialMedia.Application.Features.UserFeatures.GetCurrentUser
{
    public sealed record GetCurrentUserResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Subscription { get; set; } = string.Empty;
        public string? PhotoId { get; set; }
        public string ChatSecret { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool CanUseComchat { get; set; }
        public bool CanCall { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
