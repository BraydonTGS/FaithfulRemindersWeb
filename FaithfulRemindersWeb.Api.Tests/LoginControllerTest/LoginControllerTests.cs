using FaithfulRemindersWeb.Api.Login;
using FaithfulRemindersWeb.Api.Registration;
using FaithfulRemindersWeb.Business.Login.Dto;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Global.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests
{
    [TestClass]
    public class LoginControllerTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly RegistrationController _registrationController;
        private readonly LoginController _loginController;
        private readonly DatabaseSeeder _databaseSeeder;

        public LoginControllerTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _registrationController = _serviceProvider.GetRequiredService<RegistrationController>();
            _loginController = _serviceProvider.GetRequiredService<LoginController>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public async Task LoginUserAsync_Success()
        {
            var actionResult = await _loginController.LoginSpecifiedUserAsync(DtoGenerationHelper.GenerateLoginRequestDtoUserDtoAlreadyInDb());

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var newUser = okResult.Value as LoginResponseDto;

            Assert.IsNotNull(newUser);
        }


        [TestMethod]
        public async Task LoginUserAsync_InvalidPassword_ThrowsInvalidPasswordException_Success()
        {
            InvalidPasswordException? passwordException = null;
            try
            {
                var dto = DtoGenerationHelper.GenerateLoginRequestDtoUserDtoAlreadyInDb();

                dto.TempPassword = "WrongPassword";

                var actionResult = await _loginController.LoginSpecifiedUserAsync(dto);
            }
            catch (InvalidPasswordException ex)
            {
                passwordException = ex;
            }
            Assert.IsNotNull(passwordException);
            Assert.IsNotNull(passwordException.Message);

            Assert.IsNotNull("Password Verification Failure for the Specified User.", passwordException.Message);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
