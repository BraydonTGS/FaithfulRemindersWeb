using FaithfulRemindersWeb.Business.Business;
using FaithfulRemindersWeb.Business.Connection;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Interfaces;
using FaithfulRemindersWeb.Business.Repository;
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
            services.AddDbContext<FaithfulDbContext>(options =>
            {
                options.UseSqlServer(Hidden.GetConnectionString());
            }, ServiceLifetime.Transient);

            services.AddDbContextFactory<FaithfulDbContext>();

            // Repository //
            services.AddTransient<ToDoItemRepository>();
            services.AddTransient<UserRepository>();

            // Business Logic //
            services.AddTransient<IToDoItemBL, ToDoItemBL>();
            services.AddTransient<IUserBL, UserBL>();

            return services;
        }
    }
}
