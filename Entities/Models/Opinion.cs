using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("opinion")]
    public class Opinion : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("text")]
        [Required(ErrorMessage = "Text is required")]
        [StringLength(65535, ErrorMessage = "Text can't be longer than 65535 characters")]
        public string Text { get; set; }

        [JsonIgnore]
        [Column("exercise")]
        [Required(ErrorMessage = "Exercise ID is required")]
        public Guid ExerciseId { get; set; }

        //[JsonIgnore]
        public virtual Exercise Exercise { get; set; }

        [JsonIgnore]
        [Column("parent")]
        [Required(ErrorMessage = "Parent ID is required")]
        public Guid ParentId { get; set; }

        //[JsonIgnore]
        public virtual Parent Parent { get; set; }

        public Opinion(Guid id, string text, Guid exerciseId, Guid parentId)
        {
            Id = id;
            Text = text;
            ExerciseId = exerciseId;
            ParentId = parentId;
        }
    }
}