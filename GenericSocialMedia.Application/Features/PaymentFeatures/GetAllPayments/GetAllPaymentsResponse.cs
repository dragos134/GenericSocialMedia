namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetAllPayments
{
    public sealed record GetAllPaymentsResponse
    {
        public int UserId { get; set; }
        public string UserEmail { get; set; } = string.Empty;
        public string Subscription { get; set; } = string.Empty;
        public string PaymentMessage { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public DateTime Date { get; set; }
    }
}
