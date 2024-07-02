using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IChatUsersRepository : IBaseRepository<ChatUsers>
    {
        Task<ChatUsers?> GetByUserIds(int userId, int chatUserId);
        Task<List<ChatUsers>> GetChatUsersOfUser(int userId);
        Task<List<ChatUsers>> GetPaginatedChatUsersOfUser(int userId, int skip, int take);
    }
}
