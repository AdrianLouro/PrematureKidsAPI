using System;
using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class DoctorUser : IEntity
    {
        public Guid DoctorId { get; set; }

        public Guid UserId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Board number is required")]
        public string BoardNumber { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public string Role = "doctor";

        public Guid Id { get; set; }
    }
}