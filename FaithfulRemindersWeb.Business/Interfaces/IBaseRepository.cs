using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Interfaces
{
    internal interface IBaseRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        public Task<IEnumerable<TEntity>?> GetAllAsync();
        public Task<TEntity?> GetByIdAsync(TKey id);
        public Task<TEntity?> CreateAsync(TEntity entity);
        public Task<TEntity?> UpdateAsync(TEntity entity);
        public Task<bool> SoftDeleteAsync(TKey id);
        public Task<bool> HardDeleteAsync(TKey id);
    }
}
