using AutoMapper;
using FaithfulRemindersWeb.Business.Login;
using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace FaithfulRemindersWeb.Api.Login
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginBL _loginBL;

        public LoginController(ILoginBL loginBL, IMapper mapper) => _loginBL = loginBL;

        #region LoginSpecifiedUserAsync
        /// <summary>
        /// Attempt to Login the Specified User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(LoginSpecifiedUserAsync))]
        public async Task<ActionResult<LoginRequestDto>> LoginSpecifiedUserAsync([FromQuery] LoginRequestDto loginRequest)
        {
            var result = await _loginBL.LoginUserAsync(loginRequest);

            if (result == null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion
    }
}
