using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;

namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUserSubscription
{
    public class UpdateUserSubscriptionHandler : IRequestHandler<UpdateUserSubscriptionRequest, UpdateUserSubscriptionResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICometChatService _cometChatService;
        private readonly IMapper _mapper;
        public UpdateUserSubscriptionHandler(
            IUserRepository userRepository,
            ISubscriptionRepository subscriptionRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IPaymentRepository paymentRepository,
            ICometChatService cometChatService)
        {
            _userRepository = userRepository;
            _subscriptionRepository = subscriptionRepository;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cometChatService = cometChatService;
        }
        public async Task<UpdateUserSubscriptionResponse> Handle(UpdateUserSubscriptionRequest request, CancellationToken cancellationToken)
        {
            var payment = await _paymentRepository.Get(request.PaymentId, cancellationToken);
            var user = await _userRepository.Get(payment.UserId, cancellationToken);
            var subscription = await _subscriptionRepository.Get(payment.SubscriptionId, cancellationToken);

            await _cometChatService.CreateUser(user.Username, user.ChatSecret.ToString());

            user.Subscription = subscription;
            user.SubscriptionExpiration = DateTime.UtcNow.AddMonths(subscription.NoOfMonths ?? 0);
            user.RemainingMessages = subscription.NoOfMessages;
            _userRepository.Update(user);
            await _unitOfWork.Save(cancellationToken);
            return _mapper.Map<UpdateUserSubscriptionResponse>(null);
        }
    }
}
