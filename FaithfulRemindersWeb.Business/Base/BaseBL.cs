using AutoMapper;
using FaithfulRemindersWeb.Entity.Entities.Base;
using Serilog;


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
        private IBaseRepository<TEntity, TKey> _repository;
        protected readonly ILogger _log;
        protected readonly IMapper _mapper;

        public BaseBL(
            IBaseRepository<TEntity, TKey> repository,
            ILogger logger,
            IMapper mapper)
        {
            _repository = repository;
            _log = logger;
            _mapper = mapper;
        }

        #region GetAllAsync
        /// <summary>
        /// Retrieves all DTOs of the specified type asynchronously.
        /// </summary>
        /// <returns>A collection of DTOs or null if none are found.</returns>
        public virtual async Task<IEnumerable<TDto>?> GetAllAsync()
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting GetAllAsync for EntityType: {typeof(TEntity).Name} at {startTime}.");

            try
            {
                var entities = await _repository.GetAllAsync();

                if (entities == null)
                {
                    _log.Warning($"No entities found during GetAllAsync for EntityType: {typeof(TEntity).Name}.");
                    return null;
                }

                var dtos = _mapper.Map<IEnumerable<TDto>>(entities);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed GetAllAsync for EntityType: {typeof(TEntity).Name}. {entities.Count()} Entities Mapped to DTOs. Duration: {endTime - startTime}.");

                return dtos;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Error in GetAllAsync for EntityType: {typeof(TEntity).Name} with message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }

        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Retrieves a DTO of the specified type by its unique identifier asynchronously.
        /// </summary>
        /// <param name="key">The unique identifier of the DTO to retrieve.</param>
        /// <returns>The retrieved DTO or null if not found.</returns>
        public virtual async Task<TDto?> GetByIdAsync(TKey key)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting GetByIdAsync for EntityType: {typeof(TEntity).Name} with Specified Key at {startTime}.");

            try
            {
                var entity = await _repository.GetByIdAsync(key);

                if (entity == null)
                {
                    _log.Warning($"No Entity found with Specified Key during GetByIdAsync for EntityType: {typeof(TEntity).Name}.");
                    return null;
                }

                var dto = _mapper.Map<TDto>(entity);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed GetByIdAsync. Entity with Key mapped to DTO of Type: {typeof(TDto).Name}. Duration: {endTime - startTime}.");

                return dto;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Error in GetByIdAsync for EntityType: {typeof(TEntity).Name} with Key: {key} and message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Creates a new DTO asynchronously.
        /// </summary>
        /// <param name="dto">The DTO to be created.</param>
        /// <returns>The created DTO after it is added to the database.</returns>
        public virtual async Task<TDto?> CreateAsync(TDto dto)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting CreateAsync for DTO of Type: {typeof(TDto).Name} at {startTime}.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                if (entity == null)
                {
                    _log.Warning($"Unable to map DTO of Type {typeof(TDto).Name} to Entity. Aborting CreateAsync operation.");
                    return null;
                }

                var createdEntity = await _repository.CreateAsync(entity);
                if (createdEntity == null)
                {
                    _log.Warning($"Failed to create Entity of Type {typeof(TEntity).Name} in Database.");
                    return null;
                }

                var resultDto = _mapper.Map<TDto>(createdEntity);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed CreateAsync for DTO of Type: {typeof(TDto).Name}. Entity Creation and Mapping Successful. Duration: {endTime - startTime}.");

                return resultDto;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Exception in CreateAsync for DTO of Type: {typeof(TDto).Name}. Message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// Updates an existing entity in the database.
        /// </summary>
        /// <returns>The updated DTO after changes are saved to the database.</returns>
        public virtual async Task<TDto?> UpdateAsync(TDto dto)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting UpdateAsync for DTO of Type: {typeof(TDto).Name} at {startTime}.");

            try
            {
                var entity = _mapper.Map<TEntity>(dto);
                if (entity is null)
                {
                    _log.Warning($"UpdateAsync: Unable to map DTO to Entity. DTO Type: {typeof(TDto).Name}");
                    return null;
                }

                var results = await _repository.UpdateAsync(entity);
                if (results == null)
                {
                    _log.Warning($"UpdateAsync: Update operation failed for Entity Type: {typeof(TEntity).Name}");
                    return null;
                }

                dto = _mapper.Map<TDto>(results);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed UpdateAsync for DTO Type: {typeof(TDto).Name}. Duration: {endTime - startTime}.");

                return dto;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Exception in UpdateAsync for DTO Type: {typeof(TDto).Name}. Message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// Soft deletes an DTO by setting an IsDeleted flag.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be soft-deleted.</param>
        /// <returns>True if the soft delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> SoftDeleteAsync(TKey key)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting SoftDeleteAsync for Entity Type: {typeof(TEntity).Name} at {startTime}.");

            try
            {
                var result = await _repository.SoftDeleteAsync(key);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}. Duration: {endTime - startTime}.");

                return result;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Exception in SoftDeleteAsync for Entity Type: {typeof(TEntity).Name}. Message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// Hard deletes an entity from the database.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be hard-deleted.</param>
        /// <returns>True if the hard delete is successful; otherwise, false.</returns>
        public virtual async Task<bool> HardDeleteAsync(TKey key)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting HardDeleteAsync for Entity Type: {typeof(TEntity).Name} at {startTime}.");

            try
            {
                var result = await _repository.HardDeleteAsync(key);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed HardDeleteAsync for Entity Type: {typeof(TEntity).Name}. Duration: {endTime - startTime}.");

                return result;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Exception in HardDeleteAsync for Entity Type: {typeof(TEntity).Name} with Key: {key}. Message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion


        #region RestoreAsync
        /// <summary>
        /// Restores a soft-deleted DTO by setting the IsDeleted flag to false.
        /// </summary>
        /// <param name="key">The unique identifier of the entity to be restored.</param>
        /// <returns>True if the restoration is successful; otherwise, false.</returns>
        public virtual async Task<bool> RestoreAsync(TKey key)
        {
            var startTime = DateTime.UtcNow;

            _log.Information($"Starting RestoreAsync for Entity Type: {typeof(TEntity).Name} at {startTime}.");

            try
            {
                var result = await _repository.RestoreAsync(key);

                var endTime = DateTime.UtcNow;

                _log.Information($"Completed RestoreAsync for Entity Type: {typeof(TEntity).Name}. Duration: {endTime - startTime}.");

                return result;
            }
            catch (Exception ex)
            {
                var endTime = DateTime.UtcNow;

                _log.Error($"Exception in RestoreAsync for Entity Type: {typeof(TEntity).Name} with Key: {key}. Message: {ex.Message}. Duration: {endTime - startTime}.");

                throw;
            }
        }
        #endregion

    }
}
