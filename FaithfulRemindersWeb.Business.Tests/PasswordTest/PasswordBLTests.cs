using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Tests.Base;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class PasswordBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPasswordBL _passwordBL;
        private readonly DatabaseSeeder _databaseSeeder;

        public PasswordBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _passwordBL = _serviceProvider.GetRequiredService<IPasswordBL>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            await _databaseSeeder.Seed();
        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_passwordBL);
        }

        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
