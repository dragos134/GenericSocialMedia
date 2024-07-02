using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Entities;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(DataContext context, IUserService userService) : base(context, userService)
        {
        }

        public async Task<List<Post>> GetAllWithUser(CancellationToken cancellationToken)
        {
            return await Context.Posts.Include(x => x.User).ToListAsync(cancellationToken);
        }

        public async Task<Post?> GetUserPost(int postId, int userId, CancellationToken cancellationToken)
        {
            return await Context.Posts.Where(x => x.Id == postId && x.UserId == userId).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<List<Post>> GetUserPosts(int userId, CancellationToken cancellationToken)
        {
            return await Context.Posts.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
