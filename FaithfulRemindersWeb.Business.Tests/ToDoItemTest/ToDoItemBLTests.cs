using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
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
        public async Task GetToDoItemByIdIncludeUser_Success()
        {
            var results = await _toDoItemBL.GetToDoItemByIdIncludeUserAsync(_toDoItemId);

            Assert.IsNotNull(results);
            Assert.IsNotNull(results.User);


            Assert.IsNotNull(results);
            Assert.AreEqual(_userId, results.UserId);
            Assert.AreEqual("Cook Dinner", results.Title);
            Assert.AreEqual("Make Dinner for Tonight and Plan for Leftovers", results.Description);
            Assert.AreEqual(false, results.IsCompleted);
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

            var toDoItem = results.FirstOrDefault();

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual(_userId, toDoItem.UserId);
            Assert.AreEqual("Cook Dinner", toDoItem.Title);
            Assert.AreEqual("Make Dinner for Tonight and Plan for Leftovers", toDoItem.Description);
            Assert.AreEqual(false, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task GetAllSoftDeletedToDoItemsByUserIdAsync_Success()
        {
            var results = await _toDoItemBL.GetAllSoftDeletedToDoItemsByUserIdAsync(_userId);

            Assert.IsNotNull(results);
            Assert.AreEqual(1, results.Count());

            var toDoItem = results.FirstOrDefault();

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual(_userId, toDoItem.UserId);
            Assert.AreEqual("Workout", toDoItem.Title);
            Assert.AreEqual("Spend Time at the Gym", toDoItem.Description);
            Assert.AreEqual(true, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task CreateToDoItemAsync_Success()
        {
            var toDoItem = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(toDoItem);
            Assert.AreEqual("Add Logging", toDoItem.Title);
            Assert.AreEqual("Add Logging to the Business Logic Classes", toDoItem.Description);
            Assert.AreEqual("Think about how I want to implement logging", toDoItem.Notes);
            Assert.AreEqual(false, toDoItem.IsCompleted);
        }

        [TestMethod]
        public async Task UpdateToDoItemAsync_Success()
        {
            var results = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(results);

            results.IsCompleted = false;
            results.DueDate = DateTime.UtcNow.AddDays(7);
            results.Description = "Need to add logging before I get too far behind in my business logic";
            results.Notes = "Ask Daniel about Global Logging";

            results = await _toDoItemBL.UpdateAsync(results);

            Assert.IsNotNull(results);
            Assert.AreEqual("Add Logging", results.Title);
            Assert.AreEqual("Need to add logging before I get too far behind in my business logic", results.Description);
            Assert.AreEqual("Ask Daniel about Global Logging", results.Notes);
            Assert.AreEqual(false, results.IsCompleted);
        }

        [TestMethod]
        public async Task SoftDeleteToDoItemAsync_Success()
        {
            var todoItem = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(todoItem);

            var results = await _toDoItemBL.SoftDeleteAsync(todoItem.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allToDoItems = await _toDoItemBL.GetAllAsync();

            Assert.IsNotNull(allToDoItems);
            Assert.AreEqual(5, allToDoItems.Count());
        }

        [TestMethod]
        public async Task HardDeleteToDoItemAsync_Success()
        {
            var todoItem = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(todoItem);

            var results = await _toDoItemBL.HardDeleteAsync(todoItem.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);
        }

        [TestMethod]
        public async Task RestoreToDoItemAsync_Success()
        {
            var todoItem = await _toDoItemBL.CreateAsync(DtoGenerationHelper.GenerateToDoItemDto());

            Assert.IsNotNull(todoItem);

            await _toDoItemBL.SoftDeleteAsync(todoItem.Id);

            var results = await _toDoItemBL.RestoreAsync(todoItem.Id);

            Assert.IsNotNull(results);
            Assert.IsTrue(results);

            var allToDoItems = await _toDoItemBL.GetAllAsync();

            Assert.IsNotNull(allToDoItems);
            Assert.AreEqual(6, allToDoItems.Count());
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
          await _databaseSeeder.Clear();
        }
    }
}
