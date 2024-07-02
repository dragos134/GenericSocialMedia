using AutoMapper;
using MediatR;
using GenericSocialMedia.Application.Repositories;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetAllSubcriptions
{
    public class GetAllSubscriptionsHandler : IRequestHandler<GetAllSubscriptionsRequest, List<GetAllSubscriptionsResponse>>
    {
        private readonly IMapper _mapper;
        private readonly ISubscriptionRepository _subscriptionRepository;
        public GetAllSubscriptionsHandler(IMapper mapper, ISubscriptionRepository subscriptionRepository)
        {
            _mapper = mapper;
            _subscriptionRepository = subscriptionRepository;
        }

        public async Task<List<GetAllSubscriptionsResponse>> Handle(GetAllSubscriptionsRequest request, CancellationToken cancellationToken)
        {
            var subscriptions = await _subscriptionRepository.GetAll(cancellationToken);
            return _mapper.Map<List<GetAllSubscriptionsResponse>>(subscriptions);
        }
    }
}
