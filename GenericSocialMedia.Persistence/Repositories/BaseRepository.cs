using GenericSocialMedia.Application.Repositories;
using GenericSocialMedia.Application.Services;
using GenericSocialMedia.Domain.Common;
using GenericSocialMedia.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericSocialMedia.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly DataContext Context;
        protected readonly IUserService UserService;

        public BaseRepository(DataContext context, IUserService userService)
        {
            Context = context;
            UserService = userService;
        }

        public EntityEntry<T> Create(T entity)
        {
            var currUsername = UserService.GetUser()?.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = currUsername ?? "error";
            return Context.Add(entity);
        }

        public void Create(IEnumerable<T> entities)
        {
            Context.AddRange(entities);
        }

        public EntityEntry<T> Update(T entity)
        {
            var currUsername = UserService.GetUser()?.Claims.Where(x => x.Type == "username").Select(x => x.Value).FirstOrDefault();
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = currUsername ?? "error"; ;
            return Context.Update(entity);
        }

        public EntityEntry<T> Delete(T entity)
        {
            entity.CreatedAt = DateTimeOffset.UtcNow;
            return Context.Remove(entity);
        }

        public Task<T> Get(int id, CancellationToken cancellationToken)
        {
            return Context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public Task<List<T>> GetAll(CancellationToken cancellationToken)
        {
            return Context.Set<T>().ToListAsync(cancellationToken);
        }
    }
}
