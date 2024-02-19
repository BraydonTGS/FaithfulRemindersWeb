using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FaithfulRemindersWeb.Api.Registration
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationBL _registrationBL;

        public RegistrationController(IRegistrationBL registrationBL) => _registrationBL = registrationBL;

        #region
        /// <summary>
        /// Attempt to Register the Provided User Async
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserDto>> RegisterTheProvidedUserAsync(UserDto user)
        {
            var result = await _registrationBL.RegisterNewUserAsync(user);

            if(result is null) { return BadRequest(result); }

            return Ok(result);
        }
        #endregion
    }
}
