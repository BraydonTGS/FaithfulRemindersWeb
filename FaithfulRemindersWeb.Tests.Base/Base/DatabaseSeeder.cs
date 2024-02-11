using FaithfulRemindersWeb.Business.Context;
using FaithfulRemindersWeb.Business.Passwords;
using FaithfulRemindersWeb.Business.Passwords.Dto;
using FaithfulRemindersWeb.Entity.Entities;

namespace FaithfulRemindersWeb.Business.Tests.Base
{
    /// <summary>
    /// Database Seeder User for Seeding Test Entities
    /// </summary>
    public class DatabaseSeeder
    {
        private readonly FaithfulDbContext _context;
        private readonly IPasswordHasher<PasswordDto> _passwordHasher;

        public DatabaseSeeder(
            FaithfulDbContext context, 
            IPasswordHasher<PasswordDto> passwordHasher)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        #region Seed
        /// <summary>
        /// Seed the EF Core In-Memory Database with Entities Required for the BL and API Tests
        /// </summary>
        /// <returns></returns>
        public async Task Seed()
        {
            // User //
            var user = new User()
            {
                Id = new Guid("c0a65964-1c2d-4e59-bf3a-2b9c7a2d8c3f"),
                FirstName = "Daniel",
                LastName = "Aguirre",
                Email = "RedRain@gmail.com",
                UserName = "RedxRain",
                Notes = "JarJar of Unit Tests",

            };

            var secondUser = new User()
            {
                Id = new Guid("cf157498-c3e0-4967-886f-0e8116a2d69a"),
                FirstName = "Braydon",
                LastName = "Sutherland",
                Email = "BrayDog@gmail.com",
                UserName = "Geo",
                Notes = "App Admin",

            };

            _context.Users.Add(user);
            _context.Users.Add(secondUser);
          
            // ToDoItems //
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


            // Password //
            var password = new Password();

            var (hash, salt) = _passwordHasher.HashPassword("YodaIsMyMentor");

            password.Hash = hash;
            password.Salt = salt;
            password.UserId = user.Id;

            // Context //
            _context.Passwords.Add(password);
            _context.ToDoItems.Add(todoItemOne);
            _context.ToDoItems.Add(todoItemTwo);
            _context.ToDoItems.Add(todoItemThree);
            _context.ToDoItems.Add(todoItemFour);
            _context.ToDoItems.Add(todoItemFive);
            _context.ToDoItems.Add(todoItemSix);

            await _context.SaveChangesAsync();
        }
        #endregion

        #region Clear
        /// <summary>
        /// Clear the In-Memory Database
        /// </summary>
        /// <returns></returns>
        public async Task Clear()
        {

            _context.Users.RemoveRange(_context.Users);
            _context.ToDoItems.RemoveRange(_context.ToDoItems);
            _context.Passwords.RemoveRange(_context.Passwords);

            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
