﻿using FaithfulRemindersWeb.Entity.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FaithfulRemindersWeb.Entity.Entities
{
    /// <summary>
    /// Entity Object for the Application User
    /// </summary>
    [Table("Users")]
    public class User : BaseEntity
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid Id { get; set; }

        [Required]
        [Column("FirstName")]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Column("LastName")]
        [MaxLength(150)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("Email")]
        [MaxLength(250)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Column("UserName")]
        [MaxLength(25)]
        public string UserName { get; set; } = string.Empty;

        public IEnumerable<ToDoItem> ToDoListItems { get; set; } = Enumerable.Empty<ToDoItem>();

    }
}
