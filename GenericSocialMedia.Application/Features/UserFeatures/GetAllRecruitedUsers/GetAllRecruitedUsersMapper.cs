using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers
{
    public sealed class GetAllRecruitedUsersMapper : Profile
    {
        public GetAllRecruitedUsersMapper()
        {
            CreateMap<User, GetAllRecruitedUsersResponse>()
                .ForMember(dest => dest.IsRegistered, opt => opt.MapFrom(src => src.Password != null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime))
                .ForMember(dest => dest.Subscription, opt => opt.MapFrom(src => src.Subscription.Name));

        }
    }
}
