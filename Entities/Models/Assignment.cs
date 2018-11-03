using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("assignment")]
    public class Assignment : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Column("notes")]
        [Required(ErrorMessage = "Notes is required")]
        [StringLength(65535, ErrorMessage = "Notes can't be longer than 65535 characters")]
        public string Notes { get; set; }

        [Column("exercise_frequency")]
        [Required(ErrorMessage = "Exercise frequency is required")]
        [StringLength(255, ErrorMessage = "Exercise frequency cannot be longer then 255 characters")]
        public string ExerciseFrequency { get; set; }

        [Column("exercise_duration")]
        [Required(ErrorMessage = "Exercise duration is required")]
        [StringLength(255, ErrorMessage = "Exercise duration cannot be longer then 255 characters")]
        public string ExerciseDuration { get; set; }

        [Column("feedback_frequency")]
        [Required(ErrorMessage = "Feedback frequency is required")]
        [StringLength(255, ErrorMessage = "Feedback frequency cannot be longer then 255 characters")]
        public string FeedbackFrequency { get; set; }

        [Column("state")]
        [Required(ErrorMessage = "State is required")]
        [StringLength(255, ErrorMessage = "State cannot be longer then 255 characters")]
        public string State { get; set; }

        [JsonIgnore]
        [Column("doctor")]
        [Required(ErrorMessage = "Doctor ID is required")]
        public Guid DoctorId { get; set; }

        //[JsonIgnore]
        public virtual Doctor Doctor { get; set; }

        [JsonIgnore]
        [Column("exercise")]
        [Required(ErrorMessage = "Exercise ID is required")]
        public Guid ExerciseId { get; set; }

        //[JsonIgnore]
        public virtual Exercise Exercise { get; set; }

        [JsonIgnore]
        [Column("child")]
        [Required(ErrorMessage = "Child ID is required")]
        public Guid ChildId { get; set; }

        //[JsonIgnore]
        public virtual Child Child { get; set; }

        [JsonIgnore] public virtual IEnumerable<Session> Sessions { get; set; } = new List<Session>();

        public Assignment(Guid id, DateTime date, string notes, string exerciseFrequency, string exerciseDuration,
            string feedbackFrequency, string state, Guid doctorId, Guid exerciseId, Guid childId)
        {
            Id = id;
            Date = date;
            Notes = notes;
            ExerciseFrequency = exerciseFrequency;
            ExerciseDuration = exerciseDuration;
            FeedbackFrequency = feedbackFrequency;
            State = state;
            DoctorId = doctorId;
            ExerciseId = exerciseId;
            ChildId = childId;
        }
    }
}