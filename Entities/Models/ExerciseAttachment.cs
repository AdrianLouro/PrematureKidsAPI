using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("exercise_attachment")]
    public class ExerciseAttachment : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Column("type")]
        [Required(ErrorMessage = "Type is required")]
        [StringLength(255, ErrorMessage = "Type can't be longer than 255 characters")]
        public string Type { get; set; }

        [JsonIgnore]
        [Column("exercise")]
        [Required(ErrorMessage = "Exercise ID is required")]
        public Guid ExerciseId { get; set; }

        [JsonIgnore]
        public virtual Exercise Exercise { get; set; }

        public ExerciseAttachment(Guid id, string name, string type, Guid exerciseId)
        {
            Id = id;
            Name = name;
            Type = type;
            ExerciseId = exerciseId;
        }
    }
}