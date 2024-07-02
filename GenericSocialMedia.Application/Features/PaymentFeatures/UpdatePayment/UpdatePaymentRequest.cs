using MediatR;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.UpdatePayment
{
    public sealed record UpdatePaymentRequest(
        string Status,
        string StatusMessage,
        string? UserEmail,
        string? StripeSessionId
        ) : IRequest<UpdatePaymentResponse>;
}
