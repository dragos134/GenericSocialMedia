using GenericSocialMedia.Application.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GenericSocialMedia.Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor accessor;

        public UserService(IHttpContextAccessor accessor)
        {
            this.accessor = accessor;
        }

        public ClaimsPrincipal? GetUser()
        {
            return accessor?.HttpContext?.User;
        }
    }
}
