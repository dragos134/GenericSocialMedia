namespace GenericSocialMedia.Application.Features.UserFeatures.GetPaginatedChatUsers
{
    public sealed record GetPaginatedChatUsersResponse
    {
        public string Fullname { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string? PhotoId { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public bool IsOnline { get; set; }
        public int NoOfUnread { get; set; }
        public string Subscription { get; set; } = string.Empty;
    }
}
