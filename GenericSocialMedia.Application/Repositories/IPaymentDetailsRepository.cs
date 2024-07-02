using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IPaymentDetailsRepository : IBaseRepository<PaymentDetails>
    {
        Task<PaymentDetails> GetByPaymentId(int paymentId);

        Task<List<PaymentDetails>> GetAllPayments();

        Task<List<PaymentDetails>> GetSuccessfulPayments();
    }
}
