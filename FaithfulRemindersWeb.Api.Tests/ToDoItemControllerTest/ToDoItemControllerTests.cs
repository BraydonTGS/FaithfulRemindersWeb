using FaithfulRemindersWeb.Api.Controllers;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Api.Tests.ToDoItemControllerTest
{
    [TestClass]
    public class ToDoItemControllerTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly ToDoItemController _toDoItemController;
        private readonly DatabaseSeeder _databaseSeeder;
        private readonly Guid _userId = new Guid("c0a65964-1c2d-4e59-bf3a-2b9c7a2d8c3f");

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

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
