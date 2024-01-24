using FaithfulRemindersWeb.Business.Dto.Base;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Dto
{
    /// <summary>
    /// User Data Transfer Object
    /// </summary>
    public class UserDto : BaseDto<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public IEnumerable<ToDoItem> ToDoListItems { get; set; } = Enumerable.Empty<ToDoItem>();
    }
}
