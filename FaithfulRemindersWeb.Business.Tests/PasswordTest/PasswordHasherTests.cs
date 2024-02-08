using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.Tests.Base;
using FaithfulRemindersWeb.Business.ToDoItems;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests
{
    [TestClass]
    public class PasswordHasherTests : TestBase
    {
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _serviceProvider;
        private readonly IPasswordHasher<PasswordDto> _passwordHasher;

        private readonly DatabaseSeeder _databaseSeeder;

        public PasswordHasherTests()
        {
            _services = ConfigureServices(seedDatabase: true);
            _serviceProvider = _services.BuildServiceProvider();
            _passwordHasher = _serviceProvider.GetRequiredService<IPasswordHasher<PasswordDto>>();
            _databaseSeeder = _serviceProvider.GetRequiredService<DatabaseSeeder>();
        }

        [TestMethod]
        public void ConstructorNotNull_Success()
        {
            Assert.IsNotNull(_passwordHasher);
        }
    }
}
