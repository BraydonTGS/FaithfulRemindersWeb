using FaithfulRemindersWeb.Business.Login;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FaithfulRemindersWeb.Api.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBL _loginBL;

        public LoginController(ILoginBL loginBL) => _loginBL = loginBL;

        #region LoginSpecifiedUserAsync
        /// <summary>
        /// Attempt to Login the Specified User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(LoginSpecifiedUserAsync))]
        public async Task<ActionResult<UserDto>> LoginSpecifiedUserAsync([FromQuery] UserDto user)
        {
            var result = await _loginBL.LoginUserAsync(user);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion
    }
}
