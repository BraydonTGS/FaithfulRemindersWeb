using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Global.Enums;
using FaithfulRemindersWeb.Global.Exceptions;
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
            var password = await _passwordBL.CreatePasswordForUserAsync(_secondUserId, "IAmGroot");

            Assert.IsNotNull(password);

            Assert.AreEqual(_secondUserId, password.UserId);

            Assert.IsNotNull(password.Salt);
            Assert.IsNotNull(password.Hash);

            Assert.IsInstanceOfType(password.Hash, typeof(byte[]));
            Assert.IsInstanceOfType(password.Salt, typeof(byte[]));
        }

        [TestMethod]
        public async Task CreatePasswordForUserAsync_UserAlreadyHasPassword_Throws_PasswordAlreadyExistsException_Success()
        {
            PasswordAlreadyExistsException? passwordException = null;
            try
            {
                _ = await _passwordBL.CreatePasswordForUserAsync(_userId, "IAmGroot");
            }
            catch (PasswordAlreadyExistsException ex)
            {
                passwordException = ex;
            }
            Assert.IsNotNull(passwordException);
            Assert.IsNotNull(passwordException.Message);

            Assert.IsNotNull("A password for this user already exists.", passwordException.Message);
        }

        [TestMethod]
        public async Task VerifyUserPasswordAsync_Success()
        {
            var results = await _passwordBL.VerifyUserPasswordAsync(_userId, "YodaIsMyMentor");

            Assert.IsNotNull(results);
            Assert.AreEqual(PasswordVerificationResults.Success, results);
        }

        [TestMethod]
        public async Task VerifyUserPasswordAsync_WrongPasswordProducesFailure_Success()
        {
            var results = await _passwordBL.VerifyUserPasswordAsync(_userId, "YodaIsMySon");

            Assert.IsNotNull(results);
            Assert.AreEqual(PasswordVerificationResults.Failed, results);
        }

        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
