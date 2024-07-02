using AutoMapper;
using GenericSocialMedia.Application.Common.Helpers;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.RegisterUser
{
    public sealed class RegisterUserMapper : Profile
    {
        public RegisterUserMapper()
        {
            CreateMap<RegisterUserRequest, User>()
                .ForMember(x => x.Password, opt => opt.MapFrom(source => AuthenticationHelpers.ComputeSha256Hash(source.Password)))
                .ForMember(x => x.TermsAndConditionsAccepted, opt => opt.MapFrom(source => source.TCAccepted))
                .ForMember(x => x.RegisteredAt, opt => opt.MapFrom(source => DateTime.UtcNow));
        }
    }
}
