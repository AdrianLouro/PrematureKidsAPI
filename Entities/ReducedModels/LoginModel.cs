using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace CustomExceptionMiddleware.Models
{
    public class LoginModel : IEntity
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public Guid Id { get; set; }
    }
}