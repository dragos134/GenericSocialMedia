using GenericSocialMedia.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace GenericSocialMedia.Application.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        EntityEntry<T> Create(T entity);
        void Create(IEnumerable<T> entities);
        EntityEntry<T> Update(T entity);
        EntityEntry<T> Delete(T entity);
        Task<T> Get(int id, CancellationToken cancellationToken);
        Task<List<T>> GetAll(CancellationToken cancellationToken);
    }
}
