using System;
using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class ParentUser : IEntity
    {
        public Guid ParentId { get; set; }

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ID number is required")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Role = "parent";

        public Guid Id { get; set; }
    }
}