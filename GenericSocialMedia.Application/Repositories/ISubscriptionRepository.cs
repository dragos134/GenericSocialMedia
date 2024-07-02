using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface ISubscriptionRepository : IBaseRepository<Subscription>
    {
        Task<Subscription?> GetByName(string subscriptionName);
        Task<Subscription?> GetByStripeId(string stripeId);
    }
}
