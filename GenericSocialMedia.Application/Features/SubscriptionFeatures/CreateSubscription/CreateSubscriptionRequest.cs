using MediatR;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.CreateSubscription
{
    public sealed record CreateSubscriptionRequest : IRequest<CreateSubscriptionResponse>;
}
