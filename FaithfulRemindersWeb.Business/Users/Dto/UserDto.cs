using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;

namespace FaithfulRemindersWeb.Business.Users.Dto
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

        public IEnumerable<ToDoItemDto> ToDoListItems { get; set; } = Enumerable.Empty<ToDoItemDto>();
    }
}
