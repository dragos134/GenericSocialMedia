namespace GenericSocialMedia.Application.Common.DTOs
{
    public class StripeCheckoutSessionRequest
    {
        public string Email { get; set; } = string.Empty;
        public string StripeSubscriptionId { get; set; } = string.Empty;
    }
}
