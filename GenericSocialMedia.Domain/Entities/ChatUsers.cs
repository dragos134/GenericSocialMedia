using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class ChatUsers : BaseEntity
    {
        public User? User { get; set; }
        public int UserId { get; set; }
        public User? ChatUser { get; set; }
        public int ChatUserId { get; set; }

        public DateTime LastMessageAt { get; set; }
    }
}
