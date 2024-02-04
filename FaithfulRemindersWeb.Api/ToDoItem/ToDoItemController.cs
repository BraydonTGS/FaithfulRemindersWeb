using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FaithfulRemindersWeb.Api.ToDoItem
{
    [Route("api/ToDoItem")]
    [ApiController]
    public class ToDoItemController : ControllerBase
    {
        private readonly IToDoItemBL _toDoItemBL;
        private readonly ILogger _logger;

        public ToDoItemController(IToDoItemBL toDoItemBL, ILogger logger)
        {
            _toDoItemBL = toDoItemBL ?? throw new ArgumentNullException(nameof(toDoItemBL));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        #region Controller Methods
        #region GetAllAsync
        /// <summary>
        /// Controller - Get All ToDoItems Async
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAllAsync))]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllAsync()
        {
            var results = await _toDoItemBL.GetAllAsync();

            if (results == null) { return BadRequest(results); }

            if (!results.Any()) { return NotFound(results); }

            return Ok(results);
        }
        #endregion

        #region GetByIdAsync
        /// <summary>
        /// Controller - Get ToDoItem By Id Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetByIdAsync))]
        public async Task<ActionResult<ToDoItemDto>> GetByIdAsync(Guid key)
        {
            var result = await _toDoItemBL.GetByIdAsync(key);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region GetByIdIncludeUserAsync
        /// <summary>
        /// Controller - Get ToDoItem By Id and Include the Associated User
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetByIdIncludeUserAsync))]
        public async Task<ActionResult<ToDoItemDto>> GetByIdIncludeUserAsync(Guid key)
        {
            var result = await _toDoItemBL.GetToDoItemByIdIncludeUserAsync(key);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// Controller - Get All ToDoItemsByUserId Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAllToDoItemsByUserIdAsync))]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllToDoItemsByUserIdAsync(Guid key)
        {
            var results = await _toDoItemBL.GetAllToDoItemsByUserIdAsync(key);

            if (results == null) { return BadRequest(results); }

            if (!results.Any()) { return NotFound(results); }

            return Ok(results);
        }
        #endregion

        #region GetAllSoftDeletedToDoItemsByUserIdAsync
        /// <summary>
        /// Controller - Get All Soft Deleted ToDoItemsByUserId Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(GetAllSoftDeletedToDoItemsByUserIdAsync))]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid key)
        {
            var results = await _toDoItemBL.GetAllSoftDeletedToDoItemsByUserIdAsync(key);

            if (results == null) { return BadRequest(results); }

            if (!results.Any()) { return NotFound(results); }

            return Ok(results);
        }
        #endregion

        #region CreateAsync
        /// <summary>
        /// Controller - Create a ToDoItem Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route(nameof(CreateAsync))]
        public async Task<ActionResult<ToDoItemDto>> CreateAsync([FromBody] ToDoItemDto dto)
        {
            var result = await _toDoItemBL.CreateAsync(dto);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region UpdateAsync
        /// <summary>
        /// Controller - Update a ToDoItem Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route(nameof(UpdateAsync))]
        public async Task<ActionResult<ToDoItemDto>> UpdateAsync([FromBody] ToDoItemDto dto)
        {
            var result = await _toDoItemBL.UpdateAsync(dto);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region SoftDeleteAsync
        /// <summary>
        /// Controller - Mark a ToDoItem as SoftDeleted Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(SoftDeleteAsync))]
        public async Task<ActionResult<ToDoItemDto>> SoftDeleteAsync(Guid key)
        {
            var result = await _toDoItemBL.SoftDeleteAsync(key);

            if (result is false) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #region HardDeleteAsync
        /// <summary>
        /// Controller - Permanently Delete a TodoItem Async
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route(nameof(HardDeleteAsync))]
        public async Task<ActionResult<ToDoItemDto>> HardDeleteAsync(Guid key)
        {
            var result = await _toDoItemBL.HardDeleteAsync(key);

            if (result is false) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion

        #endregion
    }
}
