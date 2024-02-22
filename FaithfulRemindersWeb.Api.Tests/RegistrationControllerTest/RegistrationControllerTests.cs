using FaithfulRemindersWeb.Api.Registration;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users.Dto;
using FaithfulRemindersWeb.Global.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests
{
    [TestClass]
    public class RegistrationControllerTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly RegistrationController _registrationController;
        private readonly DatabaseSeeder _databaseSeeder;

        public RegistrationControllerTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _registrationController = _serviceProvider.GetRequiredService<RegistrationController>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public async Task RegisterNewUserAsync_Success()
        {
            var actionResult = await _registrationController.RegisterTheProvidedUserAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var newUser = okResult.Value as UserDto;

            Assert.IsNotNull(newUser);
        }


        [TestMethod]
        public async Task RegisterNewUserAsync_UserAlreadyHasARegisteredEmail_Throws_EmailAlreadyRegisteredException_Success()
        {

            EmailAlreadyRegisteredException? emailException = null;

            try
            {
                _ = await _registrationController.RegisterTheProvidedUserAsync(DtoGenerationHelper.GenerateUserDtoAlreadyInDb());
            }
            catch (EmailAlreadyRegisteredException ex)
            {
                emailException = ex;
            }

            Assert.IsNotNull(emailException);
            Assert.IsNotNull(emailException.Message);

            Assert.IsNotNull("The Specified Email is already Registered", emailException.Message);

        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
