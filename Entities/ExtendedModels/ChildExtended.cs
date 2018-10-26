using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class ChildExtended : IEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Medical history ID is required")]
        public string MedicalHistoryId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Doctor ID is required")]
        public Guid DoctorId { get; set; }
    }
}