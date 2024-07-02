namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUser
{
    public sealed record UpdateUserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int SubscriptionId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
