using AutoMapper;
using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Features.RoleFeatures.CreateRole
{
    public class CreateRoleMapper : Profile
    {
        public CreateRoleMapper()
        {
            CreateMap<CreateRoleRequest, Role>();
            CreateMap<Role, CreateRoleResponse>();
        }
    }
}
