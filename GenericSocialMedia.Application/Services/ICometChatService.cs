using GenericSocialMedia.Domain.ServicesModels.Cometchat;

namespace GenericSocialMedia.Application.Services
{
    public interface ICometChatService
    {
        Task CreateUser(string username, string uid, string? avatar = null, string? link = null);

        Task<CometchatListUsers?> ListUsers(string searchKey);
    }
}
