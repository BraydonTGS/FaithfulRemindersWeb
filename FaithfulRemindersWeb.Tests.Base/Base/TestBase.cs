using AutoMapper;
using FaithfulRemindersWeb.Api.ToDoItems;
using FaithfulRemindersWeb.Api.User;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Logging;
using FaithfulRemindersWeb.Business.Mapping;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Validation;
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
        protected readonly Guid _secondUserId = new Guid("cf157498-c3e0-4967-886f-0e8116a2d69a");
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
            services.AddTransient<PasswordRepository>();

            // Business Logic //
            services.AddTransient<IToDoItemBL, ToDoItemBL>();
            services.AddTransient<IUserBL, UserBL>();
            services.AddTransient<IPasswordHasher<PasswordDto>, PasswordHasher>();
            services.AddTransient<IPasswordBL, PasswordBL>();
            services.AddTransient<IRegistrationBL, RegistrationBL>();   

            // Controllers //
            services.AddTransient<ToDoItemController>();
            services.AddTransient<UserController>();

            // Validation //
            services.AddScoped<ToDoItemDtoValidator>();
            services.AddScoped<UserDtoValidator>();

            // Logging //
            LoggingConfig.ConfigureLogging(services);

            if (seedDatabase)
                services.AddScoped<DatabaseSeeder>();

            return services;
        }
    }
}
