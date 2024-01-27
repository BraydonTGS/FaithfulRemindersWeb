using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Database Seeder User for Seeding Test Entities
    /// 
    /// </summary>
    internal class DatabaseSeeder
    {
        private readonly FaithfulDbContext _context;

        public DatabaseSeeder(FaithfulDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task Seed()
        {

            var user = new User()
            {
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests",
               
            };

            _context.Users.Add(user);

            var todoItemOne = new ToDoItem
            {
                Title = "Cook Dinner",
                Description = "Make Dinner for Tonight and Plan for Leftovers",
                IsCompleted = false,
                DueDate = DateTime.UtcNow.AddHours(6),
                UserId = user.Id
            };

            var todoItemTwo = new ToDoItem
            {
                Title = "Morning Jog",
                Description = "30-minute jog around the park",
                IsCompleted = false,
                DueDate = DateTime.UtcNow.AddDays(1).AddHours(6),
                UserId = user.Id
            };

            var todoItemThree = new ToDoItem
            {
                Title = "Grocery Shopping",
                Description = "Buy groceries for the week",
                IsCompleted = false,
                DueDate = DateTime.UtcNow.AddDays(2),
                UserId = user.Id
            };

            var todoItemFour = new ToDoItem
            {
                Title = "Read a Book",
                Description = "Read 50 pages of a novel",
                IsCompleted = false,
                DueDate = DateTime.UtcNow.AddDays(1).AddHours(3),
                UserId = user.Id
            };

            var todoItemFive = new ToDoItem
            {
                Title = "Pay Bills",
                Description = "Pay the monthly utility bills",
                IsCompleted = true,
                UserId = user.Id
            };


            _context.ToDoItems.Add(todoItemOne);
            _context.ToDoItems.Add(todoItemTwo);
            _context.ToDoItems.Add(todoItemThree);
            _context.ToDoItems.Add(todoItemFour);
            _context.ToDoItems.Add(todoItemFive);

            await _context.SaveChangesAsync();
        }

        public async Task Clear()
        {

            _context.Users.RemoveRange(_context.Users);
            _context.ToDoItems.RemoveRange(_context.ToDoItems);
            await _context.SaveChangesAsync();
        }
    }
}
