using AutoMapper;
using FaithfulRemindersWeb.Business.Connection;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Logging;
using FaithfulRemindersWeb.Business.Login;
using FaithfulRemindersWeb.Business.Mapping;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Business.Registration;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.Users;
using FaithfulRemindersWeb.Business.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FaithfulRemindersWeb.Business.Config
{
    public static class BusinessServices
    {
        /// <summary>
        /// Register any Dependencies Needed for the Business Project
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureBusinessServices(IServiceCollection services)
        {
            // Database //
            services.AddDbContextFactory<FaithfulDbContext>(options =>
            {
                options.UseSqlServer(Hidden.GetConnectionString());
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
            services.AddTransient<ILoginBL, LoginBL>();

            // Validation //
            services.AddScoped<ToDoItemDtoValidator>();
            services.AddScoped<UserDtoValidator>();
            services.AddScoped<PasswordValidator>();

            // Logging //
            LoggingConfig.ConfigureLogging(services);

            return services;
        }
    }
}
