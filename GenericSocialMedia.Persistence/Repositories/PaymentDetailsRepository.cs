using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class PaymentDetailsRepository : BaseRepository<PaymentDetails>, IPaymentDetailsRepository
    {
        public PaymentDetailsRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public Task<List<PaymentDetails>> GetAllPayments()
        {
            return Context.PaymentDetails
                .Include(x => x.Payment)
                .ThenInclude(x => x.User)
                .Include(x => x.Payment)
                .ThenInclude(x => x.Subscription)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        public Task<PaymentDetails> GetByPaymentId(int paymentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<PaymentDetails>> GetSuccessfulPayments()
        {
            return Context.PaymentDetails
                .Include(x => x.Payment)
                .ThenInclude(x => x.User)
                .Include(x => x.Payment)
                .ThenInclude(x => x.Subscription)
                .Where(x => x.Status == "Success")
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();
        }
    }
}
