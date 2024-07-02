namespace GenericSocialMedia.Domain.ServicesModels.Cometchat
{
    public class CometchatMeta
    {
        public CometchatPagination Pagination { get; set; } = new();
        public CometchatCursor Cursor { get; set; } = new();
    }
}
