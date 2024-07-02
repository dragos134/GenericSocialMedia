using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context, IUserService userService) : base(context, userService)
        {

        }

        public Task<User?> GetByCredentials(string Email, string hashedPassword, CancellationToken cancellationToken)
        {
            return Context.Users.Include(x => x.Roles).Include(x => x.Subscription).FirstOrDefaultAsync(user => user.Email == Email && user.Password == hashedPassword && !user.IsDeleted, cancellationToken);
        }

        public Task<User?> GetByEmail(string email, CancellationToken cancellationToken)
        {
            return Context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Email == email && !x.IsDeleted, cancellationToken);
        }

        public Task<List<User>> GetAllUsers(CancellationToken cancellationToken)
        {
            return Context.Users
                .Include(x => x.Roles)
                .Include(x => x.Subscription)
                .Where(x => x.IsDeleted == false)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public Task<User?> GetCurrentUser(CancellationToken cancellationToken)
        {
            var currUsername = UserService.GetUser()?.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault();
            return Context.Users
                .Include(x => x.Subscription)
                .Include(x => x.Roles)
                .Where(x => x.Username == currUsername && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task UpdateUserProfilePhoto(Guid photoId, CancellationToken cancellationToken)
        {
            var user = await GetCurrentUser(cancellationToken);
            user.PhotoId = photoId;
            Context.Update(user);
        }

        public Task<User?> GetByUsername(string username, CancellationToken cancellationToken)
        {
            return Context.Users.Include(x => x.Subscription).Where(x => x.Username == username && !x.IsDeleted).FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<User>> GetChatUsers(int userId, CancellationToken cancellationToken)
        {
            return Context.Users
                .Include(x => x.SentMessages.Where(msg => msg.ReceiverId == userId))
                .Include(x => x.ReceivedMessages.Where(msg => msg.SenderId == userId))
                .Include(x => x.Subscription)
                .Where(x =>
                    (x.SentMessages.Where(y => y.ReceiverId == userId).Any() ||
                    x.ReceivedMessages.Where(y => y.SenderId == userId).Any())
                     && !x.IsDeleted)
                .Select(x => new
                {
                    UserEntity = x,
                    MergedMessages = x.SentMessages.Concat(x.ReceivedMessages).OrderByDescending(y => y.CreatedAt).AsEnumerable(),
                })
                .OrderByDescending(x => x.MergedMessages.First().CreatedAt)
                .Select(x => x.UserEntity)
                .ToListAsync(cancellationToken);
        }

        public Task<List<User>> GetPaginatedChatUsers(int userId, int skip, int take, CancellationToken cancellationToken)
        {

            return Context.Users
                .Include(x => x.SentMessages.Where(msg => msg.ReceiverId == userId))
                .Include(x => x.ReceivedMessages.Where(msg => msg.SenderId == userId))
                .Include(x => x.Subscription)
                .Where(x =>
                    (x.SentMessages.Where(y => y.ReceiverId == userId).Any() ||
                    x.ReceivedMessages.Where(y => y.SenderId == userId).Any())
                     && !x.IsDeleted)
                .Select(x => new
                {
                    UserEntity = x,
                    MergedMessages = x.SentMessages.Concat(x.ReceivedMessages).OrderByDescending(y => y.CreatedAt).AsEnumerable(),
                })
                .OrderByDescending(x => x.MergedMessages.First().CreatedAt)
                .Select(x => x.UserEntity)
                .Skip(skip)
                .Take(take)
                .ToListAsync(cancellationToken);
        }

        public async Task<List<User>> SearchUsers(string searchText, CancellationToken cancellationToken)
        {
            return await Context.Users
                 .Where(x => x.Username.ToLower().Contains(searchText) || x.FirstName.ToLower().Contains(searchText) ||
                 x.LastName.ToLower().Contains(searchText) || ((x.FirstName + " " + x.LastName).ToLower().Contains(searchText) && !x.IsDeleted))
                 .Take(10)
                 .ToListAsync();
        }

        public Task<List<User>> GetAllRegisteredUsers(CancellationToken cancellationToken)
        {
            return Context.Users
                .Include(x => x.Roles)
                .Include(x => x.Subscription)
                .Where(x => x.IsDeleted == false && x.Roles.Where(x => x.Name == "User").Count() == 1 && x.Password != null && !x.IsDeleted)
                .OrderByDescending(x => x.RegisteredAt)
                .ToListAsync();
        }

        public Task<User?> GetByChatConnectionId(string chatConnectionId, CancellationToken cancellationToken)
        {
            return Context.Users
                .Where(x => x.ChatConnectionId == chatConnectionId)
                .FirstOrDefaultAsync();
        }
    }
}
