using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Entity.Entities.Base;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FaithfulRemindersWeb.Api.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TDto, TEntity, TKey> : ControllerBase
        where TDto : BaseDto<TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly IBaseBL<TDto, TEntity, TKey> _baseBL;
        protected readonly ILogger _log;

        public BaseController(IBaseBL<TDto, TEntity, TKey> baseBL, ILogger logger)
        {
            _log = logger ?? throw new ArgumentNullException(nameof(logger));
            _baseBL = baseBL ?? throw new ArgumentNullException(nameof(baseBL));
        }

        #region GetAllAsync
        /// <summary>
        /// BaseController - Get All Async
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAllAsync))]
        public async Task<ActionResult<IEnumerable<TDto>?>> GetAllAsync()
        {
            var results = await _baseBL.GetAllAsync();

            if (results == null) { return BadRequest(results); }

            if (!results.Any()) { return NotFound(results); }

            return Ok(results);
        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// BaseController - Get By Id Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetByIdAsync))]
        public async Task<ActionResult<ToDoItemDto>> GetByIdAsync(TKey key)
        {
            var result = await _baseBL.GetByIdAsync(key);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// BaseController - Create Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CreateAsync))]
        public async Task<ActionResult<ToDoItemDto>> CreateAsync([FromBody] TDto dto)
        {
            var result = await _baseBL.CreateAsync(dto);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// BaseController - Update Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(UpdateAsync))]
        public async Task<ActionResult<ToDoItemDto>> UpdateAsync([FromBody] TDto dto)
        {
            var result = await _baseBL.UpdateAsync(dto);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// BaseController - Mark an Entity as SoftDeleted Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(SoftDeleteAsync))]
        public async Task<ActionResult<ToDoItemDto>> SoftDeleteAsync(TKey key)
        {
            var result = await _baseBL.SoftDeleteAsync(key);

            if (result is false) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// BaseController - Permanently Delete an Entity Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(HardDeleteAsync))]
        public async Task<ActionResult<ToDoItemDto>> HardDeleteAsync(TKey key)
        {
            var result = await _baseBL.HardDeleteAsync(key);

            if (result is false) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// BaseController - Restore an Entity that is Flagged as Deleted
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(RestoreAsync))]
        public async Task<ActionResult<ToDoItemDto>> RestoreAsync(TKey key)
        {
            var result = await _baseBL.RestoreAsync(key);

            if (result is false) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion
    }
}
