using GenericSocialMedia.Domain.Entities;
namespace GenericSocialMedia.Application.Repositories
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<List<Post>> GetAllWithUser(CancellationToken cancellationToken);

        Task<List<Post>> GetUserPosts(int userId, CancellationToken cancellationToken);

        Task<Post?> GetUserPost(int postId, int userId, CancellationToken cancellationToken);
    }
}
