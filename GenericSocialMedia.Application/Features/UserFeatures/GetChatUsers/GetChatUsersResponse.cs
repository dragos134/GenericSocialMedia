namespace GenericSocialMedia.Application.Features.UserFeatures.GetChatUsers
{
    public sealed record GetChatUsersResponse
    {
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? PhotoId { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public int NoOfUnread { get; set; }
        public string Subscription { get; set; } = string.Empty;
    }
}
