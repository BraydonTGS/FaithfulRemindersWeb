using FaithfulRemindersWeb.Api.Login;
using FaithfulRemindersWeb.Api.Registration;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests.LoginControllerTest
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
            await _registrationController.RegisterTheProvidedUserAsync(DtoGenerationHelper.GenerateUserDto());

            var actionResult = await _loginController.LoginSpecifiedUserAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var newUser = okResult.Value as UserDto;

            Assert.IsNotNull(newUser);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
