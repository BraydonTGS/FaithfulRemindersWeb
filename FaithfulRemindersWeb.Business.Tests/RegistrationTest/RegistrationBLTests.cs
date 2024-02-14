using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.Tests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class RegistrationBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRegistrationBL _registrationBL;
        private readonly DatabaseSeeder _databaseSeeder;

        public RegistrationBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _registrationBL = _serviceProvider.GetRequiredService<IRegistrationBL>();
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

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
