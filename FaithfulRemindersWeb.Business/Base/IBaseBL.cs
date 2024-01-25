
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IBaseBL<TDto, TEntity, TKey> 
        where TDto : BaseDto<TKey>
        where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TDto>?> GetAllAsync();
    }
}
