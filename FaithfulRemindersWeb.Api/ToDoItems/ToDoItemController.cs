using FaithfulRemindersWeb.Api.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FaithfulRemindersWeb.Api.ToDoItems
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoItemController : BaseController<ToDoItemDto, ToDoItem, Guid>
    {
        private readonly IToDoItemBL _toDoItemBL;

        public ToDoItemController(IToDoItemBL toDoItemBL, ILogger logger) : base(toDoItemBL, logger) => _toDoItemBL = toDoItemBL;

        #region Controller Methods
        #region GetByIdIncludeUserAsync
        /// <summary>
        /// Controller - Get ToDoItem By Id and Include the Associated User
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route(nameof(GetAllSoftDeletedToDoItemsByUserIdAsync))]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid key)
        {
            var results = await _toDoItemBL.GetAllSoftDeletedToDoItemsByUserIdAsync(key);

            if (results == null) { return BadRequest(results); }

            if (!results.Any()) { return NotFound(results); }

            return Ok(results);
        }
        #endregion
        #endregion
    }
}
