using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class SupportTicket : BaseEntity
    {
        public string Message { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
