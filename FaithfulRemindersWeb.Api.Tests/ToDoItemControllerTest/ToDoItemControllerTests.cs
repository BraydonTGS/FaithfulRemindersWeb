using FaithfulRemindersWeb.Api.Controllers;
using FaithfulRemindersWeb.Business.Tests.Base;
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
            var results = await _toDoItemController.GetAllAsync();

            Assert.IsNotNull(results);

            Assert.AreEqual(5, results.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
