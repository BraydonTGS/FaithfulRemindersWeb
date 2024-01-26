using AutoMapper;
using FaithfulRemindersWeb.Entity.Entities.Base;

namespace FaithfulRemindersWeb.Business.Base
{
    /// <summary>
    /// Generic Base Business Logic used for interacting with the <see cref="IBaseRepository{TEntity, TKey}"/>
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
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TDto>?> GetAllAsync()
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

        #region CreateAsync
        /// <summary>
        /// Generic Create Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public virtual async Task<TDto?> CreateAsync(TDto dto)
        {
            try
            {
                var entity = _mapper.Map<TEntity>(dto);

                if (entity == null) return null;

                var results = await _repository.CreateAsync(entity);

                if (results == null) return null;

                dto = _mapper.Map<TDto>(results);

                return dto;
            }
            catch (Exception)
            {

                throw;
            }
        }
        #endregion

    }
}
