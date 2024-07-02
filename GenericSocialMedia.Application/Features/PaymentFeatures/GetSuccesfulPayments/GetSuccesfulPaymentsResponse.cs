namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetSuccesfulPayments
{
    public sealed record GetSuccesfulPaymentsResponse
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Subscription { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
