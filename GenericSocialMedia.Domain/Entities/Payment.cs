using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class Payment : BaseEntity
    {
        public int UserId { get; set; }
        public User User { get; set; } = new();
        public int SubscriptionId { get; set; }
        public Subscription Subscription { get; set; } = new();

        // stripe ids
        public string StripeSessionId { get; set; } = string.Empty;
        public string? StripePaymentIntentId { get; set; }
        public List<PaymentDetails> PaymentDetails { get; set; } = new();
    }
}
