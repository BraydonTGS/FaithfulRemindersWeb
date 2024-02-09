using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Used for Generating DTOs for Test Classes and Methods
    /// </summary>
    public static class DtoGenerationHelper
    {
        #region GenerateToDoItemDto
        public static ToDoItemDto GenerateToDoItemDto()
        {
            var toDoItem = new ToDoItemDto()
            {
                Title = "Add Logging",
                Description = "Add Logging to the Business Logic Classes",
                Notes = "Think about how I want to implement logging",
                DueDate = DateTime.UtcNow.AddDays(5),
                IsCompleted = false,
            };

            return toDoItem;
        }
        #endregion

        #region GenerateUserDto
        public static UserDto GenerateUserDto()
        {
            var userDto = new UserDto()
            {
                FirstName = "Braydon",
                LastName = "Sutherland",
                Email = "BraydonTGS@gmail.com",
                UserName = "Geomatics",
                Notes = "Application Owner"
            };
            return userDto;
        }
        #endregion

        #region GeneratePasswordDto
        public static PasswordDto GeneratePasswordDto()
        {
            return new PasswordDto();
        }
        #endregion
    }
}
