using AutoMapper;
using GenericSocialMedia.Application.Common.Helpers;
using GenericSocialMedia.Application.Features.UserFeatures.GetAllRecruitedUsers;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.CreateUser
{
    public sealed class CreateUserMapper : Profile
    {
        public CreateUserMapper()
        {
            CreateMap<CreateUserRequest, User>();
            CreateMap<User, CreateUserResponse>()
                .ForMember(dest => dest.IsRegistered, opt => opt.MapFrom(src => src.Password != null))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt.UtcDateTime));
        }
    }
}
