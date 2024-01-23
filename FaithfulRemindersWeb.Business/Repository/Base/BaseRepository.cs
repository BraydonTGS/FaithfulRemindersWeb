using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Repository.Base
{
    internal class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<TEntity?> CreateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>?> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> GetByIdAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HardDeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SoftDeleteAsync(object id)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity?> UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
