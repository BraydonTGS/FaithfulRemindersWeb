
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Entity.Entities;
using FaithfulRemindersWeb.Global.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class UserBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IUserBL _userBL;
        private readonly DatabaseSeeder _databaseSeeder;

        public UserBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _userBL = _serviceProvider.GetRequiredService<IUserBL>();
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
            Assert.IsNotNull(_userBL);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_Success()
        {
            var results = await _userBL.GetAllAsync();

            Assert.IsNotNull(results);

            Assert.AreEqual(2, results.Count());
        }

        [TestMethod]
        public async Task GetUserByIdAsync_Success()
        {
            var results = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(results);

            var user = await _userBL.GetByIdAsync(results.Id);

            Assert.IsNotNull(user);
            Assert.AreEqual("Braydon", user.FirstName);
            Assert.AreEqual("Sutherland", user.LastName);
            Assert.AreEqual("BraydonTGS@gmail.com", user.Email);
            Assert.AreEqual(false, user.IsDeleted);
        }

        [TestMethod]
        public async Task CreateUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(user);
            Assert.AreEqual("Braydon", user.FirstName);
            Assert.AreEqual("Sutherland", user.LastName);
            Assert.AreEqual("BraydonTGS@gmail.com", user.Email);
            Assert.AreEqual(false, user.IsDeleted);
        }

        [TestMethod]
        public async Task CreateUserAsync_UserAlreadyHasARegisteredEmail_Throws_EmailAlreadyRegisteredException_Success()
        {
            EmailAlreadyRegisteredException? emailException = null;

            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());
            Assert.IsNotNull(user);

            try
            {
                _ = await _userBL.CreateAsync(user);
            }
            catch (EmailAlreadyRegisteredException ex)
            {
                emailException = ex;
            }

            Assert.IsNotNull(emailException);
            Assert.IsNotNull(emailException.Message);

            Assert.IsNotNull("The Specified Email is already Registered", emailException.Message);
        }

        [TestMethod]
        public async Task UpdateUserAsync_Success()
        {
            var results = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(results);

            results.FirstName = "Braydon";
            results.LastName = "Sutherland";
            results.UserName = "Geo-Matics";
            results.Email = "BraydonTGSuds@gmail.com";

            results = await _userBL.UpdateAsync(results);

            Assert.IsNotNull(results);
            Assert.AreEqual("Braydon", results.FirstName);
            Assert.AreEqual("Sutherland", results.LastName);
            Assert.AreEqual("BraydonTGSuds@gmail.com", results.Email);
            Assert.AreEqual(false, results.IsDeleted);
        }

        [TestMethod]
        public async Task SoftDeleteUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(user);

            var results = await _userBL.SoftDeleteAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allUsers = await _userBL.GetAllAsync();

            Assert.IsNotNull(allUsers);
            Assert.AreEqual(2, allUsers.Count());
        }

        [TestMethod]
        public async Task HardDeleteUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(user);

            var results = await _userBL.HardDeleteAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreUserAsync_Success()
        {
            var user = await _userBL.CreateAsync(DtoGenerationHelper.GenerateUserDto());

            Assert.IsNotNull(user);

            await _userBL.SoftDeleteAsync(user.Id);

            var results = await _userBL.RestoreAsync(user.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allUsers = await _userBL.GetAllAsync();

            Assert.IsNotNull(allUsers);
            Assert.AreEqual(3, allUsers.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
