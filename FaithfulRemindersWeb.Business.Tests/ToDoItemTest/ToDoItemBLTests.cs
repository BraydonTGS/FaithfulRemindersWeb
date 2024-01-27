using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class ToDoItemBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IToDoItemBL _toDoItemBL;
        private readonly DatabaseSeeder _databaseSeeder;
        private readonly Guid _userId = new Guid("c0a65964-1c2d-4e59-bf3a-2b9c7a2d8c3f");


        public ToDoItemBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _toDoItemBL = _serviceProvider.GetRequiredService<IToDoItemBL>();
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
            Assert.IsNotNull(_toDoItemBL);
        }

        [TestMethod]
        public async Task GetAllToDoItemsAsync_Success()
        {
            var results = await _toDoItemBL.GetAllAsync();

            Assert.IsNotNull(results);

            Assert.AreEqual(5, results.Count());
        }

        [TestMethod]
        public async Task GetToDoItemByIdAsync_Success()
        {
            var results = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(results);

            var toDoItem = await _toDoItemBL.GetByIdAsync(results.Id);

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Add Logging", toDoItem.Title);
            Assert.AreEqual("Add Logging to the Business Logic Classes", toDoItem.Description);
            Assert.AreEqual("Think about how I want to implement logging", toDoItem.Notes);
            Assert.AreEqual(false, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task GetAllToDoItemsByUserIdAsync_Success()
        {
            var results = await _toDoItemBL.GetAllToDoItemsByUserIdAsync(_userId);

            Assert.IsNotNull(results);
            Assert.AreEqual(5, results.Count());
        }

        [TestMethod]
        public async Task CreateToDoItemAsync_Success()
        {
            var toDoItem = DtoGenerationHelper.GenerateToDoItemDto();

            var results = await _toDoItemBL.CreateAsync(toDoItem);

            Assert.IsNotNull(results);
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
