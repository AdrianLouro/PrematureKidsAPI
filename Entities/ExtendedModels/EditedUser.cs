using System;
using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class EditedUser : IEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Current password is required")]
        public string CurrentPassword { get; set; }

        [MinLength(8, ErrorMessage = "New password must have at least 8 characters")]
        public string NewPassword { get; set; }
    }
}