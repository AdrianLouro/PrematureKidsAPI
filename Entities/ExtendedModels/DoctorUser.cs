﻿using System;
using System.ComponentModel.DataAnnotations;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class DoctorUser : IEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Board number is required")]
        public string BoardNumber { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        public string Telephone { get; set; }

        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        //[MinLength(8, ErrorMessage = "Password must have at least 8 characters")]
        //public string Password { get; set; }

        public string Role = "doctor";
    }
}