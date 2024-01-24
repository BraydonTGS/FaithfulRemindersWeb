using FaithfulRemindersWeb.Business.Connection;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FaithfulRemindersWeb.Business.Context
{
    /// <summary>
    /// Application DB Context - Faithful Reminders
    /// </summary>
    public class FaithfulDbContext : DbContext
    {
        public DbSet<ToDoItem> ToDoItems { get; set; }

        public DbSet<User> Users { get; set; }

        public FaithfulDbContext() { }
        public FaithfulDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer(Hidden.GetConnectionString())
                    .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }, LogLevel.Information);
        }

        // Configure Fluent API //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
