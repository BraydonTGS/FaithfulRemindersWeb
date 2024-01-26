﻿using FaithfulRemindersWeb.Business.Tests.Base;
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
        public async Task TestCleanup()
        {
            await _databaseSeeder.Clear();
        }
    }
}
