﻿using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FaithfulRemindersWeb.Api.Controllers
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
        /// Get All ToDoItems Async
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllAsync()
        {
            try
            {
                var results = await _toDoItemBL.GetAllAsync();

                if (results == null) { return BadRequest(results); }

                if (!results.Any()) { return NotFound(results); }

                return Ok(results);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAllToDoItemsByUserIdAsync
        /// <summary>
        /// Get All ToDoItemsByUserId Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllToDoItemsByUserIdAsync(Guid key)
        {
            try
            {
                var results = await _toDoItemBL.GetAllToDoItemsByUserIdAsync(key);

                if (results == null) { return BadRequest(results); }

                if (!results.Any()) { return NotFound(results); }

                return Ok(results);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region GetAllSoftDeletedToDoItemsByUserIdAsync
        /// <summary>
        /// Get All Soft Deleted ToDoItemsByUserId Async
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToDoItemDto>?>> GetAllSoftDeletedToDoItemsByUserIdAsync(Guid key)
        {
            try
            {
                var results = await _toDoItemBL.GetAllSoftDeletedToDoItemsByUserIdAsync(key);

                if (results == null) { return BadRequest(results); }

                if (!results.Any()) { return NotFound(results); }

                return Ok(results);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
        #endregion
    }
}
