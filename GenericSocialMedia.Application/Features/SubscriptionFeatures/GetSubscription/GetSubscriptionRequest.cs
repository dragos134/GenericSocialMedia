using MediatR;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetSubscription
{
    public sealed record GetSubscriptionRequest(int Id) : IRequest<GetSubscriptionResponse>;

}
