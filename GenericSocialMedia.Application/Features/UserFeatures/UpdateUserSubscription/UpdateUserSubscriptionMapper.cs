using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUserSubscription
{
    public sealed class UpdateUserSubscriptionMapper : Profile
    {
        public UpdateUserSubscriptionMapper()
        {
            CreateMap<User, UpdateUserSubscriptionResponse>();
        }
    }
}
