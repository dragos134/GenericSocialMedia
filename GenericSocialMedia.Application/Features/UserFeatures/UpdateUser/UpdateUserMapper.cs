using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.UserFeatures.UpdateUser
{
    public sealed class UpdateUserMapper : Profile
    {
        public UpdateUserMapper()
        {
            CreateMap<UpdateUserRequest, User>();
            CreateMap<User, UpdateUserResponse>();
        }
    }
}
