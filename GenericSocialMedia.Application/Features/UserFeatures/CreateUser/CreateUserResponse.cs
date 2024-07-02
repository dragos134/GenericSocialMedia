namespace GenericSocialMedia.Application.Features.UserFeatures.CreateUser
{
    public sealed record CreateUserResponse
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Email { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public bool IsRegistered { get; set; }
    }
}
