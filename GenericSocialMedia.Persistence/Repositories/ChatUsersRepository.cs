using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class ChatUsersRepository : BaseRepository<ChatUsers>, IChatUsersRepository
    {
        public ChatUsersRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public Task<ChatUsers?> GetByUserIds(int userId, int chatUserId)
        {
            return Context.ChatUsers
                .Where(x => x.UserId == userId && x.ChatUserId == chatUserId)
                .FirstOrDefaultAsync();
        }

        public Task<List<ChatUsers>> GetChatUsersOfUser(int userId)
        {
            return Context.ChatUsers
                .Include(x => x.ChatUser)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.LastMessageAt)
                .ToListAsync();
        }

        public Task<List<ChatUsers>> GetPaginatedChatUsersOfUser(int userId, int skip, int take)
        {
            return Context.ChatUsers
                .Include(x => x.ChatUser)
                .ThenInclude(y => y.Subscription)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.LastMessageAt)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
    }
}
