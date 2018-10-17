using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("user")]
    public class User : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("email")]
        [Required(ErrorMessage = "Email is required")]
        [StringLength(255, ErrorMessage = "Email can't be longer than 255 characters")]
        public string Email { get; set; }

        [Column("password")]
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Password can't be longer than 255 characters")]
        public string Password { get; set; }

        [Column("role")]
        [Required(ErrorMessage = "Role is required")]
        [StringLength(255, ErrorMessage = "Telephone cannot be longer then 255 characters")]
        public string Role { get; set; }

        public User(Guid id, string email, string password, string role)
        {
            Id = id;
            Email = email;
            Password = password;
            Role = role;
        }
    }
}