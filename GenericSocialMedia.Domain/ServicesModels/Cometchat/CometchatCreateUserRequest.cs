namespace GenericSocialMedia.Domain.ServicesModels.Cometchat
{
    public class CometchatCreateUserRequest
    {
        public string Uid { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string? Link { get; set; }
        public string? Role { get; set; }
        public List<string>? Tags { get; set; }
        public bool? WithAuthToken { get; set; }
    }
}
