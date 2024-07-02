
namespace GenericSocialMedia.Application.Features.UserFeatures.AuthenticateUser
{
    public sealed record ValidateUserCredentialsResponse
    {
        public string Email { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public int Id { get; set; }
        public string ChatSecret { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;
        public bool CanUseComchat { get; set; }
        public bool CanCall { get; set; }
        public List<string> Roles { get; set; } = new();
    }
}
