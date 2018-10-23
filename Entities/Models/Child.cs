using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("child")]
    public class Child : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Column("date_of_birth")]
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }

        [Column("gender")]
        [Required(ErrorMessage = "Gender is required")]
        [StringLength(255, ErrorMessage = "Gender cannot be longer then 255 characters")]
        public string Gender { get; set; }

        [JsonIgnore]
        public virtual IEnumerable<ChildParent> Parents { get; set; } = new List<ChildParent>();

        [JsonIgnore]
        public virtual IEnumerable<ChildDoctor> Doctors { get; set; } = new List<ChildDoctor>();

        public Child(Guid id, string name, DateTime dateOfBirth, string gender)
        {
            Id = id;
            Name = name;
            DateOfBirth = dateOfBirth;
            Gender = gender;
        }
    }
}