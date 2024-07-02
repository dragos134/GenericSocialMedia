using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<Role?> GetRoleByName(string roleName);
    }
}
