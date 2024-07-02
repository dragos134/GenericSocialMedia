using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string? Description { get; set; }
        public Guid? PhotoId { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
