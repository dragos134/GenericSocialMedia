using GenericSocialMedia.Domain.Common;

namespace GenericSocialMedia.Domain.Entities
{
    public class Subscription : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string StripeId { get; set; } = string.Empty;
        public int? NoOfMonths { get; set; }
        public int NoOfMessages { get; set; }
        public float Price { get; set; }
        public bool CanUseComChat { get; set; }
        public bool CanUseCall { get; set; }
    }
}
