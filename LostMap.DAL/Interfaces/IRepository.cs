using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LostMap.DAL.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity? GetFirst(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        List<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true);

        void Create(TEntity entity);

        void Create(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Update(IEnumerable<TEntity> entities);

        void Delete(TEntity entity);
    }
}
