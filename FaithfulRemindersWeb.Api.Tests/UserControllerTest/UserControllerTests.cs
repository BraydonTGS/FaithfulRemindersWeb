using FaithfulRemindersWeb.Api.ToDoItems;
using FaithfulRemindersWeb.Api.User;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using FaithfulRemindersWeb.Business.Users.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests.UserControllerTest
{
    [TestClass]
    public class UserControllerTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly UserController _userController;
        private readonly DatabaseSeeder _databaseSeeder;

        public UserControllerTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _userController = _serviceProvider.GetRequiredService<UserController>();
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
            Assert.IsNotNull(_userController);
        }

        [TestMethod]
        public async Task GetAllUsersAsync_Success()
        {
            var actionResult = await _userController.GetAllAsync();

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var items = okResult.Value as IEnumerable<UserDto>;

            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count());
        }

        [TestMethod]
        public async Task GetUserByIdAsync_Success()
        {
            var actionResult = await _userController.GetByIdAsync(_userId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var user = okResult.Value as UserDto;

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task CreateUserAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateUserDto();

            var actionResult = await _userController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var user = okResult.Value as UserDto;

            Assert.IsNotNull(user);
        }

        [TestMethod]
        public async Task UpdateUserAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateUserDto();

            var actionResult = await _userController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var user = okResult.Value as UserDto;

            Assert.IsNotNull(user);

            user.UserName = "Geo";

            actionResult = await _userController.UpdateAsync(user);

            okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            user = okResult.Value as UserDto;

            Assert.IsNotNull(user);
            Assert.AreEqual("Geo", user.UserName);
        }

        [TestMethod]
        public async Task SoftDeleteUserAsync_Success()
        {
            var actionResult = await _userController.SoftDeleteAsync(_userId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isSoftDeleted = (bool)okResult.Value;

            Assert.IsTrue(isSoftDeleted);
        }

        [TestMethod]
        public async Task HardDeleteUserAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateUserDto();

            var actionResult = await _userController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var user = okResult.Value as UserDto;

            Assert.IsNotNull(user);

            actionResult = await _userController.HardDeleteAsync(user.Id);

            Assert.IsNotNull(actionResult);

            okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isDeleted = (bool)okResult.Value;

            Assert.IsTrue(isDeleted);
        }

        [TestMethod]
        public async Task RestoreUserAsync_Success()
        {
            var actionResult = await _userController.SoftDeleteAsync(_userId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isSoftDeleted = (bool)okResult.Value;

            Assert.IsTrue(isSoftDeleted);

            actionResult = await _userController.RestoreAsync(_userId);

            Assert.IsNotNull(actionResult);

            okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isRestored = (bool)okResult.Value;

            Assert.IsTrue(isRestored);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
