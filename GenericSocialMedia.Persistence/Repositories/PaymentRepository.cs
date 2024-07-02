using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class PaymentRepository : BaseRepository<Payment>, IPaymentRepository
    {
        public PaymentRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }
        public async Task<Payment?> GetLastByUserEmail(string email)
        {
            return await Context.Payments.Include(x => x.User).Where(x => x.User.Email == email).OrderByDescending(x => x.CreatedAt).FirstOrDefaultAsync();
        }

        public async Task<Payment?> GetByStripeSessionId(string stripeSessionId)
        {
            return await Context.Payments.Where(x => x.StripeSessionId == stripeSessionId).FirstOrDefaultAsync();
        }
    }
}
