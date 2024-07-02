using System.Security.Claims;

namespace GenericSocialMedia.Application.Services
{
    public interface IUserService
    {
        ClaimsPrincipal? GetUser();
    }
}
