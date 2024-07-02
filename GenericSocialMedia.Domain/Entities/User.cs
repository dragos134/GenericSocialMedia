using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = string.Empty;
        public string? Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Gender { get; set; }
        public string? Password { get; set; }
        public Guid ChatSecret { get; set; }
        public bool TermsAndConditionsAccepted { get; set; }
        public string? City { get; set; }
        public int? RemainingMessages { get; set; }
        public DateTime? SubscriptionExpiration { get; set; }
        public int SubscriptionId { get; set; }
        public Guid? PhotoId { get; set; }
        public string? ChatConnectionId { get; set; }
        public bool IsOnline { get; set; }
        public DateTime? RegisteredAt { get; set; }
        public Subscription Subscription { get; set; } = new();

        public List<Role> Roles { get; set; } = new();

        public ICollection<Message> ReceivedMessages { get; set; } = new List<Message>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();

        public ICollection<ChatUsers> ChatUsers { get; set; } = new List<ChatUsers>();
        public ICollection<ChatUsers> IsChatUsersFor { get; set; } = new List<ChatUsers>();
        public List<Payment>? Payments { get; set; }

    }
}
