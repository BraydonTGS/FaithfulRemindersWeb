using AutoMapper;
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Base Business Logic
    /// </summary>
    /// <typeparam name="TDto"></typeparam>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseBL<TDto, TEntity, TKey> : IBaseBL<TDto, TEntity, TKey> 
        where TDto : BaseDto<TKey>
        where TEntity : BaseEntity<TKey>
    {
        IBaseRepository<TEntity, TKey> _repository;
        private readonly IMapper _mapper;

        public BaseBL(IBaseRepository<TEntity, TKey> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        #region GetAllAsync
        /// <summary>
        /// Generic Get All Async
        /// 
        /// Query the Repository for all Entities of type TDto
        /// 
        /// Map results from TEntity to TDto
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<TDto>?> GetAllAsync()
        {
            try
            {
                var entities = await _repository.GetAllAsync();

                if (entities == null) return null;

                var results = _mapper.Map<IEnumerable<TDto>>(entities);

                return results;
            }
            catch (Exception)
            { 
                throw;
            }
        }
        #endregion

    }
}
