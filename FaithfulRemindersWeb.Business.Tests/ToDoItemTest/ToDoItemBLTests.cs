using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.ToDoItems.Dto;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business
{
    [TestClass]
    public class ToDoItemBLTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IToDoItemBL _toDoItemBL;

        public ToDoItemBLTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _toDoItemBL = _serviceProvider.GetRequiredService<IToDoItemBL>();
        }

        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_toDoItemBL);
        }

        [TestMethod]
        public async Task CreateToDoItemAsync_Success()
        {
            var toDoItem = new ToDoItemDto()
            {
                Title = "Add Logging",
                Description = "Add Logging to the Business Logic Classes",
                DueDate = DateTime.UtcNow.AddDays(5),
                IsCompleted = false,
            };

            var results = await _toDoItemBL.CreateAsync(toDoItem);

            Assert.IsNotNull(results);
        }

        [TestCleanup]
        public void TestCleanup()
        {

        }
    }
}
