﻿using AutoMapper;
using FaithfulRemindersWeb.Business.Context;
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
        public virtual IServiceCollection ConfigureServices(bool seedDatabase = false)
        {
            var services = new ServiceCollection();

            // Database //
            services.AddDbContext<FaithfulDbContext>(options =>
            {
                options.UseInMemoryDatabase(databaseName: $"InMemoryDB: {Guid.NewGuid()}");
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

            return services;
        }
    }
}