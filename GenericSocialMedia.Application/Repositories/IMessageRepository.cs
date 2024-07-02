using GenericSocialMedia.Domain.Entities;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IMessageRepository : IBaseRepository<Message>
    {
        Task<List<Message>> GetConversation(int userId1, int userId2);
        Task<Message?> GetWithUsers(int messageId, CancellationToken cancellationToken);
        Task<List<Message>> GetUserMessages(int userId, CancellationToken cancellationToken);
        Task<List<Message>> GetUnreadMessagesFromConversation(int senderId, int receiverId, CancellationToken cancellationToken);
        Task<List<Message>> GetSentMessagesWithReceivers(int userId, CancellationToken cancellationToken);
        Task<List<Message>> GetReceivedMessagesWithSenders(int userId, CancellationToken cancellationToken);
    }
}
