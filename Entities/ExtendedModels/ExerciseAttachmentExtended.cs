using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class ExerciseAttachmentExtended : IEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Exercise ID is required")]
        public Guid ExerciseId { get; set; }

        public string FullPath { get; set; }

        public ExerciseAttachmentExtended(ExerciseAttachment exerciseAttachment)
        {
            Id = exerciseAttachment.Id;
            Name = exerciseAttachment.Name;
            Type = exerciseAttachment.Type;
            ExerciseId = exerciseAttachment.ExerciseId;
            FullPath = "https//localhost:5000/ExercisesAttachments/" + Name;
        }
    }
}