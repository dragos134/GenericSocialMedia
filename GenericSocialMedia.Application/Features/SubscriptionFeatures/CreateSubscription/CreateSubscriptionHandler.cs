using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.CreateSubscription
{
    public class CreateSubscriptionHandler : IRequestHandler<CreateSubscriptionRequest, CreateSubscriptionResponse>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public CreateSubscriptionHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }

        public async Task<CreateSubscriptionResponse> Handle(CreateSubscriptionRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CreateSubscriptionResponse>(null);
        }
    }
}
