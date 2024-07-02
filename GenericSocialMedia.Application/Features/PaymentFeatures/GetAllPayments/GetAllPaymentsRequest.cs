using MediatR;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.GetAllPayments
{
    public sealed record GetAllPaymentsRequest : IRequest<List<GetAllPaymentsResponse>>;
}
