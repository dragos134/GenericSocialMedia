using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.PaymentFeatures.CreatePayment
{
    public sealed class CreatePaymentHandler : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IPaymentDetailsRepository _paymentDetailsRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;
        public CreatePaymentHandler(
            IUnitOfWork unitOfWork,
            IPaymentRepository paymentRepository,
            IPaymentDetailsRepository paymentDetailsRepository,
            IUserRepository userRepository,
            ISubscriptionRepository subscriptionRepository,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _paymentRepository = paymentRepository;
            _paymentDetailsRepository = paymentDetailsRepository;
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmail(request.UserEmail, cancellationToken);
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var subscription = await _subscriptionRepository.GetByStripeId(request.StripeSubscriptionId);
            if (subscription == null)
            {
                throw new ArgumentException(nameof(subscription));
            }
            var paymentToAdd = new Payment
            {
                User = user,
                Subscription = subscription,
                StripeSessionId = request.StripeSessionId,
            };

            var responsePayment = _paymentRepository.Create(paymentToAdd);

            var paymentDetailToAdd = new PaymentDetails
            {
                Payment = responsePayment.Entity,
                Status = "Pending",
                StatusMessage = "Checkout visited"
            };
            var responsePaymentDetail = _paymentDetailsRepository.Create(paymentDetailToAdd);

            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<CreatePaymentResponse>(paymentToAdd);
        }
    }
}
