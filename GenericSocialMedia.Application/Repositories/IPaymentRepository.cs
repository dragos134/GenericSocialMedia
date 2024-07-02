using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IPaymentRepository : IBaseRepository<Payment>
    {
        Task<Payment?> GetLastByUserEmail(string email);
        Task<Payment?> GetByStripeSessionId(string stripeSessionId);
    }
}
