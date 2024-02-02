using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Database Seeder User for Seeding Test Entities
    /// </summary>
    public class DatabaseSeeder
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
                Id = new Guid("c0a65964-1c2d-4e59-bf3a-2b9c7a2d8c3f"),
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests",

            };

            _context.Users.Add(user);

            var todoItemOne = new ToDoItem
            {
                Id = new Guid("4f82bc9a-7e6d-4e4f-8a2b-1d5e6a7b8c9f"),
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

            var todoItemSix = new ToDoItem
            {
                Title = "Workout",
                Description = "Spend Time at the Gym",
                IsCompleted = true,
                IsDeleted = true,
                UserId = user.Id
            };


            _context.ToDoItems.Add(todoItemOne);
            _context.ToDoItems.Add(todoItemTwo);
            _context.ToDoItems.Add(todoItemThree);
            _context.ToDoItems.Add(todoItemFour);
            _context.ToDoItems.Add(todoItemFive);
            _context.ToDoItems.Add(todoItemSix);

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
