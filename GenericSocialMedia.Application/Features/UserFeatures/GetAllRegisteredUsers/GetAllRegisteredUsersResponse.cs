namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRegisteredUsers
{
    public sealed record GetAllRegisteredUsersResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public DateTime? RegisteredAt { get; set; }
        public string Subscription { get; set; } = string.Empty;
    }
}
