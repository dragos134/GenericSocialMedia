using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class PaymentDetails : BaseEntity
    {
        public int PaymentId { get; set; }
        public Payment Payment { get; set; } = new();

        public string Status { get; set; } = string.Empty;
        public string? StatusMessage { get; set; }
        public string? StripeChargeId { get; set; }
    }
}
