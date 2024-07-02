using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePaymentDetails
{
    public class CreatePaymentDetailsHandler : IRequestHandler<CreatePaymentDetailsRequest, CreatePaymentDetailsResponse>
    {
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CreatePaymentDetailsHandler(
            IPaymentDetailsRepository paymentDetailsRepository,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _paymentDetailsRepository = paymentDetailsRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CreatePaymentDetailsResponse> Handle(CreatePaymentDetailsRequest request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.GetLastByUserEmail(request.UserEmail);
            if (payment == null)
            {
                throw new ArgumentNullException(nameof(payment));
            }

            var paymentDetailToCreate = new PaymentDetails
            {
                Payment = payment,
                Status = request.Status,
                StatusMessage = request.StatusMessage,
                StripeChargeId = request.StripeChargeId,
            };

            _paymentDetailsRepository.Create(paymentDetailToCreate);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<CreatePaymentDetailsResponse>(payment.Id);
        }
    }
}
