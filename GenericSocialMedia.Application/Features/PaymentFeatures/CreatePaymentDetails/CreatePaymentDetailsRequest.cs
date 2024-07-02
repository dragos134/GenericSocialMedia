using MediatR;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePaymentDetails
{
    public sealed record CreatePaymentDetailsRequest(
        string UserEmail,
        string StripeChargeId,
        string Status,
        string StatusMessage) : IRequest<CreatePaymentDetailsResponse>;
}
