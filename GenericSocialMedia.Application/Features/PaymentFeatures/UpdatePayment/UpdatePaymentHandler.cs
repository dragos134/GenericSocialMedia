using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.UpdatePayment
{
    public class UpdatePaymentHandler : IRequestHandler<UpdatePaymentRequest, UpdatePaymentResponse>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdatePaymentHandler(IPaymentRepository paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<UpdatePaymentResponse> Handle(UpdatePaymentRequest request, CancellationToken cancellationToken)
        {
            Payment? payment = new();
            if (request.UserEmail != null)
            {
                payment = await _paymentRepository.GetLastByUserEmail(request.UserEmail);
            }
            if (request.StripeSessionId != null)
            {
                payment = await _paymentRepository.GetByStripeSessionId(request.StripeSessionId);
            }

            if (payment == null)
            {
                throw new Exception();
            }

            _paymentRepository.Update(payment);
            return null;
        }
    }
}
