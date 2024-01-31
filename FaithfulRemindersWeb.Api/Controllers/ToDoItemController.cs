using FaithfulRemindersWeb.Business.ToDoItems;
using Microsoft.AspNetCore.Mvc;
using ILogger = Serilog.ILogger;

namespace FaithfulRemindersWeb.Api.Controllers
{
    [Route("api/[controller]")]
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
    }
}
