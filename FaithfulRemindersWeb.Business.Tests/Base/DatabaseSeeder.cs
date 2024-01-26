using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Database Seeder User for Seeding Test Entities
    /// 
    /// Context Factor to ensure proper disposal and prevent memory leaks
    /// </summary>
    internal class DatabaseSeeder
    {
        private readonly IDbContextFactory<FaithfulDbContext> _contextFactory;

        public DatabaseSeeder(IDbContextFactory<FaithfulDbContext> contextFactory)
        {
            _contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        public async Task Seed()
        {
            using var context = await _contextFactory.CreateDbContextAsync();

            var user = new User()
            {
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests"
            };

            context.Users.Add(user);

            await context.SaveChangesAsync();
        }

        public async Task Clear()
        {
            using var context = await _contextFactory.CreateDbContextAsync();
            context.Users.RemoveRange(context.Users);
            await context.SaveChangesAsync();   
        }
    }
}
