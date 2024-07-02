using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmail(string email, CancellationToken cancellationToken);

        Task<User?> GetByUsername(string username, CancellationToken cancellationToken);
        Task<User?> GetByCredentials(string Username, string hashedPassword, CancellationToken cancellationToken);

        Task<List<User>> GetAllUsers(CancellationToken cancellationToken);
        Task<List<User>> GetAllRegisteredUsers(CancellationToken cancellationToken);
        Task<User?> GetCurrentUser(CancellationToken cancellationToken);
        Task<List<User>> GetChatUsers(int userId, CancellationToken cancellationToken);

        Task<List<User>> GetPaginatedChatUsers(int userId, int skip, int take, CancellationToken cancellationToken);
        Task UpdateUserProfilePhoto(Guid photoId, CancellationToken cancellationToken);

        Task<List<User>> SearchUsers(string searchText, CancellationToken cancellationToken);

        Task<User?> GetByChatConnectionId(string chatConnectionId, CancellationToken cancellationToken);
    }
}
