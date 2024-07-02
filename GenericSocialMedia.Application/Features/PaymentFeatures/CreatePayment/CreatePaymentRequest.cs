using MediatR;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePayment
{
    public sealed record CreatePaymentRequest(
        string UserEmail,
        string StripeSessionId,
        string StripeSubscriptionId
        ) : IRequest<CreatePaymentResponse>;
}
