using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
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

        [TestCleanup]
        public void TestCleanup()
        {

        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull( _toDoItemBL );
        }
    }
}
