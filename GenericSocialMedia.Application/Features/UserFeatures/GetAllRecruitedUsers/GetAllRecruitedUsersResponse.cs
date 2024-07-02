namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers
{
    public sealed record GetAllRecruitedUsersResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public bool IsRegistered { get; set; }
        public string Subscription { get; set; } = string.Empty;
        public int SubscriptionId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
