using LostMap.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace LostMap.DAL.Repositories
{
    internal class BaseRepository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {
        protected readonly AppDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public BaseRepository(AppDbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }

        public virtual TEntity? GetFirst(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return GetQuery(predicate, include, useTracking).FirstOrDefault();
        }

        public virtual async Task<TEntity?> GetFirstAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return await GetQuery(predicate, include, useTracking).FirstOrDefaultAsync();
        }

        public virtual TEntity? GetSingle(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return GetQuery(predicate, include, useTracking).SingleOrDefault();
        }

        public virtual async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return await GetQuery(predicate, include, useTracking).SingleOrDefaultAsync();
        }

        public virtual List<TEntity> Get(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return GetQuery(predicate, include, useTracking).ToList();
        }

        public virtual async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            return await GetQuery(predicate, include, useTracking).ToListAsync();
        }

        private IQueryable<TEntity> GetQuery(Expression<Func<TEntity, bool>>? predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null,
            bool useTracking = true)
        {
            var query = useTracking ? DbSet : DbSet.AsNoTracking();

            if (predicate != null)
                query = query.Where(predicate);

            if (include != null)
                query = include(query);

            return query;
        }

        public virtual void Create(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Create(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Update(TEntity entity)
        {
            DbSet.Update(entity);
        }

        public virtual void Update(IEnumerable<TEntity> entities)
        {
            DbSet.UpdateRange(entities);
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
        }
    }
}
