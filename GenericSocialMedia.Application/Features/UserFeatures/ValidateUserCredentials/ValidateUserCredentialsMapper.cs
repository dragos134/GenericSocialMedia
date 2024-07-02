using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.AuthenticateUser
{
    public class ValidateUserCredentialsMapper : Profile
    {
        public ValidateUserCredentialsMapper()
        {
            CreateMap<User, ValidateUserCredentialsResponse>()
                .ForMember(dest => dest.Roles, opt => opt.MapFrom(src => src.Roles.Select(x => x.Name).ToList()))
                .ForMember(dest => dest.ChatSecret, opt => opt.MapFrom(src => src.ChatSecret.ToString()))
                .ForMember(dest => dest.CanUseComchat, opt => opt.MapFrom(src => src.Subscription.CanUseComChat))
                .ForMember(dest => dest.CanCall, opt => opt.MapFrom(src => src.Subscription.CanUseCall));
        }
    }
}
