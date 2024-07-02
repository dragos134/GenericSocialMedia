namespace GenericSocialMedia.Application.Features.UserFeatures.CheckUsernameExists
{
    public sealed record CheckUsernameExistsResponse
    {
        public bool UsernameExists { get; set; }
    }
}
