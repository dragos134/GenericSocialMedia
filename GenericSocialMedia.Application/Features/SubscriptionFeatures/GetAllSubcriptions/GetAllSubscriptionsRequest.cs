using MediatR;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetAllSubcriptions
{
    public sealed record GetAllSubscriptionsRequest : IRequest<List<GetAllSubscriptionsResponse>>;
}
