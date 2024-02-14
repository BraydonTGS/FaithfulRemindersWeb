using FaithfulRemindersWeb.Business.Users.Dto;

namespace FaithfulRemindersWeb.Business.Registration
{
    /// <summary>
    /// Interface to Define the Required Implementation for a Registration Service
    /// </summary>
    public interface IRegistrationBL
    {
        Task<UserDto?> RegisterNewUserAsync(UserDto user);
    }
}