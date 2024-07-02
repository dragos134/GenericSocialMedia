using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public async Task<Role?> GetRoleByName(string roleName)
        {
            return await Context.Roles.Where(x => x.Name == roleName).FirstOrDefaultAsync();
        }
    }
}
