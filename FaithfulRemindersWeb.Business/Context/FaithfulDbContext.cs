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

        public DbSet<UserRole> Roles { get; set; }  

        public DbSet<Password> Passwords { get; set; }

        public FaithfulDbContext() { }
        public FaithfulDbContext(DbContextOptions options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder
                    .UseSqlServer(Hidden.GetConnectionString());
        }

        // Configure Fluent API //
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
