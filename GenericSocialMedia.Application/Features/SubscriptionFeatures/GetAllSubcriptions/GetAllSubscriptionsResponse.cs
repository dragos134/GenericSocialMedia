namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetAllSubcriptions
{
    public sealed record GetAllSubscriptionsResponse
    {
        public string StripeId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
