namespace GenericSocialMedia.Domain.ServicesModels.Cometchat
{
    public class CometchatListUsers
    {
        public List<CometchatUser> Data { get; set; } = new();
        public CometchatMeta Meta { get; set; } = new();
    }
}
