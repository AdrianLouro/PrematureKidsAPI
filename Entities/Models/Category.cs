using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("category")]
    public class Category : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [JsonIgnore] public virtual IEnumerable<Exercise> Exercises { get; set; } = new List<Exercise>();

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}