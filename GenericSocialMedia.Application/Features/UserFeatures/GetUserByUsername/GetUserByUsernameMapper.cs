using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetUserByUsername
{
    public sealed class GetUserByUsernameMapper : Profile
    {
        public GetUserByUsernameMapper()
        {
            CreateMap<User, GetUserByUsernameResponse>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(source => $"{source.FirstName} {source.LastName}"))
                .ForMember(dest => dest.PhotoId, opt => opt.MapFrom(source => source.PhotoId.ToString()))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(source => source.Subscription.Name));
        }
    }
}
