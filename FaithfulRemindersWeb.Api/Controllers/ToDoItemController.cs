using FaithfulRemindersWeb.Business.ToDoItems;
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

        [HttpGet]
        public async Task<IEnumerable<ToDoItemDto>?> GetAllAsync()
        {
            try
            {
                var results = await _toDoItemBL.GetAllAsync();

                if (results == null) { return null; }

                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
