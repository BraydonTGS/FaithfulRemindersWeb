using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Global.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class RegistrationBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRegistrationBL _registrationBL;
        private readonly IUserBL _userBL;
        private readonly DatabaseSeeder _databaseSeeder;

        public RegistrationBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _registrationBL = _serviceProvider.GetRequiredService<IRegistrationBL>();
            _userBL = _serviceProvider.GetRequiredService<IUserBL>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public async Task RegisterNewUserAsync_ReturnsNewUserSuccess()
        {
            var newUser = DtoGenerationHelper.GenerateUserDto();

            newUser = await _registrationBL.RegisterNewUserAsync(newUser);

            Assert.IsNotNull(newUser);

        }


        [TestMethod]
        public async Task RegisterNewUserAsync_UserAlreadyHasARegisteredEmail_Throws_EmailAlreadyRegisteredException_Success()
        {

            EmailAlreadyRegisteredException? emailException = null;

            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(user);

            try
            {
                _ = await _registrationBL.RegisterNewUserAsync(user);
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
