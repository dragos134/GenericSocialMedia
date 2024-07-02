using MediatR;

namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUserSubscription
{
    public sealed record UpdateUserSubscriptionRequest(int PaymentId) : IRequest<UpdateUserSubscriptionResponse>;
}
