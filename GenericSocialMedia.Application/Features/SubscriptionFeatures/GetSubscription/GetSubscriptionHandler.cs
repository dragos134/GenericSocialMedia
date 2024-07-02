using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetSubscription
{
    public class GetSubscriptionHandler : IRequestHandler<GetSubscriptionRequest, GetSubscriptionResponse>
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public GetSubscriptionHandler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public async Task<GetSubscriptionResponse> Handle(GetSubscriptionRequest request, CancellationToken cancellationToken)
        {
            return _mapper.Map<GetSubscriptionResponse>(null);
        }
    }
}
