using AutoMapper;
using FaithfulRemindersWeb.Api.ToDoItem;
using FaithfulRemindersWeb.Business.Connection;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Logging;
using FaithfulRemindersWeb.Business.Mapping;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Base Class for any Business Logic Test Classes
    /// Used for Configuring Dependencies and Seeding Data if Needed
    /// </summary>
    public abstract class TestBase
    {
        protected readonly Guid _userId = new Guid("c0a65964-1c2d-4e59-bf3a-2b9c7a2d8c3f");
        protected readonly Guid _toDoItemId = new Guid("4f82bc9a-7e6d-4e4f-8a2b-1d5e6a7b8c9f");

        public virtual IServiceCollection ConfigureServices(bool seedDatabase = false)
        {
            var services = new ServiceCollection();

            // Database //
            services.AddDbContextFactory<FaithfulDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: $"InMemoryDB: {Guid.NewGuid()}");
            });

            // AutoMapper //
            var mapperConfig = new MapperConfiguration(map =>
            {
                map.AddProfile<MappingProfile>();
            });

            services.AddSingleton(mapperConfig.CreateMapper());

            // Repository //
            services.AddTransient<ToDoItemRepository>();
            services.AddTransient<UserRepository>();

            // Business Logic //
            services.AddTransient<IToDoItemBL, ToDoItemBL>();
            services.AddTransient<IUserBL, UserBL>();

            // Controllers //
            services.AddTransient<ToDoItemController>();

            // Logging //
            LoggingConfig.ConfigureLogging(services);

            if (seedDatabase)
                services.AddScoped<DatabaseSeeder>();

            return services;
        }
    }
}
