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
        /// Controller - Get All Async
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
    }
}
