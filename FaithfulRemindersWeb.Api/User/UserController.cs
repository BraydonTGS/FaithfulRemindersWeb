using FaithfulRemindersWeb.Api.Base;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FaithfulRemindersWeb.Api.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<UserDto, Entity.Entities.User, Guid>
    {
        public UserController(IUserBL userBL, Serilog.ILogger logger) : base(userBL, logger)
        {
        }

    }
}
