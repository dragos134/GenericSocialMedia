using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class MessageRepository : BaseRepository<Message>, IMessageRepository
    {
        public MessageRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public async Task<List<Message>> GetConversation(int userId1, int userId2)
        {
            return await Context.Messages
                .Include(message => message.Sender)
                .Include(message => message.Receiver)
                .Where(message =>
                    (message.SenderId == userId1 && message.ReceiverId == userId2) ||
                    (message.SenderId == userId2 && message.ReceiverId == userId1))
                .ToListAsync();
        }

        public Task<List<Message>> GetUnreadMessagesFromConversation(int senderId, int receiverId, CancellationToken cancellationToken)
        {
            return Context.Messages
                .Where(msg => msg.SenderId == senderId && msg.ReceiverId == receiverId && !msg.IsRead)
                .ToListAsync(cancellationToken);
        }

        public Task<Message?> GetWithUsers(int messageId, CancellationToken cancellationToken)
        {
            return Context.Messages
                .Include(msg => msg.Receiver)
                .Include(msg => msg.Sender)
                .Where(msg => msg.Id == messageId)
                .FirstOrDefaultAsync(cancellationToken);
        }

        public Task<List<Message>> GetUserMessages(int userId, CancellationToken cancellationToken)
        {
            return Context.Messages
                .Include(msg => msg.Sender)
                .Include(msg => msg.Receiver)
                .Where(msg => msg.ReceiverId == userId || msg.SenderId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public Task<List<Message>> GetSentMessagesWithReceivers(int userId, CancellationToken cancellationToken)
        {
            return Context.Messages
                .Include(msg => msg.Receiver)
                .Where(msg => msg.SenderId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public Task<List<Message>> GetReceivedMessagesWithSenders(int userId, CancellationToken cancellationToken)
        {
            return Context.Messages
                .Include(msg => msg.Sender)
                .Where(msg => msg.ReceiverId == userId)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }
    }
}
