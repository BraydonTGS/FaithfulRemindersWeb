using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Interfaces
{
    internal interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        public Task<IEnumerable<TEntity>?> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(object id);
        public Task<TEntity?> CreateAsync(TEntity entity);
        public Task<TEntity?> UpdateAsync(TEntity entity);
        public Task<bool> SoftDeleteAsync(object id);
        public Task<bool> HardDeleteAsync(object id);
    }
}
