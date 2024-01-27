using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using System.Diagnostics;

namespace FaithfulRemindersWeb.Business.Users.Dto
{
    /// <summary>
    /// User Data Transfer Object
    /// </summary>
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public class UserDto : BaseDto<Guid>
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserName { get; set; } = string.Empty;

        public ICollection<ToDoItemDto>? ToDoListItems { get; set; };

        private string GetDebuggerDisplay()
        {
            return $"FirstName: {FirstName}, LastName: {LastName}";
        }
    }
}
