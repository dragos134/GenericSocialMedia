using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class SubscriptionRepository : BaseRepository<Subscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public async Task<Subscription?> GetByName(string subscriptionName)
        {
            return await Context.Subscriptions.Where(x => x.Name == subscriptionName).FirstOrDefaultAsync();
        }

        public async Task<Subscription?> GetByStripeId(string stripeId)
        {
            return await Context.Subscriptions.Where(x => x.StripeId == stripeId).FirstOrDefaultAsync();
        }
    }
}
