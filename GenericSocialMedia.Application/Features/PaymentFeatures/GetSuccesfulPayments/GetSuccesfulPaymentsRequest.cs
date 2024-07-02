using MediatR;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetSuccesfulPayments
{
    public sealed record GetSuccesfulPaymentsRequest : IRequest<List<GetSuccesfulPaymentsResponse>>;
}
