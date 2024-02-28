using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Identity.Data;

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
                TempPassword = "MonkeyDBanana",
                UserName = "Geomatics",
                Notes = "Application Owner"
            };
            return userDto;
        }
        #endregion

        #region GenerateUserDtoAlreadyInDb
        public static UserDto GenerateUserDtoAlreadyInDb()
        {
            var userDto = new UserDto()
            {
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests",
                TempPassword = "YodaIsMyMentor"
            };
            return userDto;
        }
        #endregion

        #region GenerateLoginRequestDto
        public static LoginRequestDto GenerateLoginRequestDto()
        {
            var loginRequestDto = new LoginRequestDto()
            {

                Email = "BraydonTGS@gmail.com",
                TempPassword = "MonkeyDBanana",
                UserName = "Geomatics",
                Notes = "Application Owner"
            };
            return loginRequestDto;
        }
        #endregion

        #region GenerateLoginRequestDtoUserDtoAlreadyInDb
        public static LoginRequestDto GenerateLoginRequestDtoUserDtoAlreadyInDb()
        {
            var loginRequestDto = new LoginRequestDto()
            {
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests",
                TempPassword = "YodaIsMyMentor"
            };
            return loginRequestDto;
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
