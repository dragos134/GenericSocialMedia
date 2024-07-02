namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllUsers
{
    public sealed record GetAllUsersResponse
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Subscription { get; set; } = string.Empty;
    }
}
