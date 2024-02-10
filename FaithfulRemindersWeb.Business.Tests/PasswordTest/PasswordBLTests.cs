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

        [TestMethod]
        public async Task CreatePasswordForUserAsync_Success()
        {
            var password = await _passwordBL.CreatePasswordForUserAsync(_userId, "IAmGroot");

            Assert.IsNotNull(password);

            Assert.AreEqual(_userId, password.UserId);

            Assert.IsNotNull(password.Salt);
            Assert.IsNotNull(password.Hash);

            Assert.IsInstanceOfType(password.Hash, typeof(byte[]));
            Assert.IsInstanceOfType(password.Salt, typeof(byte[]));

            var testing = await _passwordBL.GetAllAsync();
        }



        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
