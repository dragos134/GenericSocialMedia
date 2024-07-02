using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.SubscriptionFeatures.GetAllSubcriptions
{
    public sealed class GetAllSubscriptionsMapper : Profile
    {
        public GetAllSubscriptionsMapper()
        {
            CreateMap<Subscription, GetAllSubscriptionsResponse>();
        }
    }
}
