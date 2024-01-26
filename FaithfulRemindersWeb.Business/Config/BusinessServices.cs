﻿using AutoMapper;
using FaithfulRemindersWeb.Business.Connection;
using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Logging;
using FaithfulRemindersWeb.Business.Mapping;
using FaithfulRemindersWeb.Business.ToDoItems;
using FaithfulRemindersWeb.Business.Users;
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

            // Logging //
            LoggingConfig.ConfigureLogging(services);

            return services;
        }
    }
}