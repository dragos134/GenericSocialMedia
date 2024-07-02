using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetCurrentUser
{
    public sealed class GetCurrentUserMapper : Profile
    {
        public GetCurrentUserMapper()
        {
            CreateMap<User, GetCurrentUserResponse>()
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Subscription.Name))
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(src => src.PhotoId.ToString()))
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Name).ToList()))
                .ForMember(dest => dest.ChatSecret, opt => opt.MapFrom(src => src.ChatSecret.ToString()))
                .ForMember(dest => dest.CanUseComchat, opt => opt.MapFrom(src => src.Subscription.CanUseComChat))
                .ForMember(dest => dest.CanCall, opt => opt.MapFrom(src => src.Subscription.CanUseCall));
        }
    }
}
