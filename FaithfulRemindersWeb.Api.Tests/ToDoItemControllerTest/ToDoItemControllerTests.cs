using FaithfulRemindersWeb.Api.ToDoItems;
using FaithfulRemindersWeb.Api.User;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests
{
    [TestClass]
    public class ToDoItemControllerTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly ToDoItemController _toDoItemController;
        private readonly DatabaseSeeder _databaseSeeder;
        public ToDoItemControllerTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _toDoItemController = _serviceProvider.GetRequiredService<ToDoItemController>();
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
            Assert.IsNotNull(_toDoItemController);
        }

        [TestMethod]
        public async Task GetAllToDoItemsAsync_Success()
        {
            var actionResult = await _toDoItemController.GetAllAsync();

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var items = okResult.Value as IEnumerable<ToDoItemDto>;

            Assert.IsNotNull(items);
            Assert.AreEqual(5, items.Count());
        }

        [TestMethod]
        public async Task GetToDoItemByIdIncludeUserAsync_Success()
        {
            var actionResult = await _toDoItemController.GetByIdIncludeUserAsync(_toDoItemId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var item = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public async Task GetAllToDoItemsByUserIdAsync_Success()
        {
            var actionResult = await _toDoItemController.GetAllToDoItemsByUserIdAsync(_userId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var items = okResult.Value as IEnumerable<ToDoItemDto>;

            Assert.IsNotNull(items);
            Assert.AreEqual(5, items.Count());
        }

        [TestMethod]
        public async Task GetToDoItemByIdAsync_Success()
        {
            var actionResult = await _toDoItemController.GetByIdAsync(_toDoItemId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var item = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public async Task GetAllToDoItemsByUserIdAsync_NotFound_Success()
        {
            var actionResult = await _toDoItemController.GetAllToDoItemsByUserIdAsync(Guid.NewGuid());

            Assert.IsNotNull(actionResult);

            var notFoundResult = actionResult.Result as NotFoundObjectResult;

            Assert.IsNotNull(notFoundResult);
            Assert.AreEqual(notFoundResult.StatusCode, 404);
        }

        [TestMethod]
        public async Task GetAllSoftDeletedToDoItemsByUserIdAsync_Success()
        {
            var actionResult = await _toDoItemController.GetAllSoftDeletedToDoItemsByUserIdAsync(_userId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var items = okResult.Value as IEnumerable<ToDoItemDto>;

            Assert.IsNotNull(items);
            Assert.AreEqual(1, items.Count());
        }

        [TestMethod]
        public async Task CreateToDoItemAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateToDoItemDto();

            var actionResult = await _toDoItemController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var toDoItem = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Add Logging", toDoItem.Title);
            Assert.AreEqual("Add Logging to the Business Logic Classes", toDoItem.Description);
            Assert.AreEqual("Think about how I want to implement logging", toDoItem.Notes);
            Assert.AreEqual(false, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task UpdateToDoItemAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateToDoItemDto();

            var actionResult = await _toDoItemController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var toDoItem = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(toDoItem);

            toDoItem.Title = "Add Logging to Controller";
            toDoItem.Description = "Add Logging to the ToDoItem Controller";
            toDoItem.Notes = string.Empty;

            actionResult = await _toDoItemController.UpdateAsync(toDoItem);

            okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            toDoItem = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Add Logging to Controller", toDoItem.Title);
            Assert.AreEqual("Add Logging to the ToDoItem Controller", toDoItem.Description);
            Assert.AreEqual("", toDoItem.Notes);
            Assert.AreEqual(false, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task SoftDeleteToDoItemAsync_Success()
        {
            var actionResult = await _toDoItemController.SoftDeleteAsync(_toDoItemId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isSoftDeleted = (bool)okResult.Value;

            Assert.IsTrue(isSoftDeleted);
        }

        [TestMethod]
        public async Task HardDeleteToDoItemAsync_Success()
        {
            var dto = DtoGenerationHelper.GenerateToDoItemDto();

            var actionResult = await _toDoItemController.CreateAsync(dto);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);

            var toDoItem = okResult.Value as ToDoItemDto;

            Assert.IsNotNull(toDoItem);

            actionResult = await _toDoItemController.HardDeleteAsync(toDoItem.Id);

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
            var actionResult = await _toDoItemController.SoftDeleteAsync(_toDoItemId);

            Assert.IsNotNull(actionResult);

            var okResult = actionResult.Result as OkObjectResult;

            Assert.IsNotNull(okResult);
            Assert.AreEqual(okResult.StatusCode, 200);
            Assert.IsNotNull(okResult.Value);

            var isSoftDeleted = (bool)okResult.Value;

            Assert.IsTrue(isSoftDeleted);

            actionResult = await _toDoItemController.RestoreAsync(_toDoItemId);

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
