using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRegisteredUsers
{
    public sealed class GetAllRegisteredUsersMapper : Profile
    {
        public GetAllRegisteredUsersMapper()
        {
            CreateMap<User, GetAllRegisteredUsersResponse>()
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Subscription.Name));
        }
    }
}
