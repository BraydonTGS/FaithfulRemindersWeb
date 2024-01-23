using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Repository.Base
{
    /// <summary>
    /// Base Repository for Basic CRUD Functionality
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    internal class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<TEntity?> CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByIdAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardDeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(TKey id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
