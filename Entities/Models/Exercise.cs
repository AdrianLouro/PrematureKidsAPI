using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("exercise")]
    public class Exercise : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("title")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(255, ErrorMessage = "Title can't be longer than 255 characters")]
        public string Title { get; set; }

        [Column("description")]
        [Required(ErrorMessage = "Description is required")]
        [StringLength(65535, ErrorMessage = "Description can't be longer than 65535 characters")]
        public string Description { get; set; }

        [JsonIgnore]
        [Column("category")]
        [Required(ErrorMessage = "Category ID is required")]
        public Guid CategoryId { get; set; }

        //[JsonIgnore]
        public virtual Category Category { get; set; }

        [JsonIgnore]
        [Column("doctor")]
        [Required(ErrorMessage = "Doctor ID is required")]
        public Guid DoctorId { get; set; }

        //[JsonIgnore]
        public virtual Doctor Doctor { get; set; }

        [JsonIgnore] public virtual IEnumerable<Opinion> Opinions { get; set; } = new List<Opinion>();

        [JsonIgnore]
        public virtual IEnumerable<ExerciseAttachment> Attachments { get; set; } = new List<ExerciseAttachment>();

        public Exercise(Guid id, string title, string description, Guid categoryId, Guid doctorId)
        {
            Id = id;
            Title = title;
            Description = description;
            CategoryId = categoryId;
            DoctorId = doctorId;
        }
    }
}