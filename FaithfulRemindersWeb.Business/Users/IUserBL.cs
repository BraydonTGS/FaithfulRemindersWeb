using FaithfulRemindersWeb.Business.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Users
{
    public interface IUserBL : IBaseBL<UserDto, User, Guid>
    {
    }
}
