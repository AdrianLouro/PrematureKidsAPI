using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("session")]
    public class Session : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("date")]
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        [Column("parent_notes")]
        [Required(ErrorMessage = "Parent notes is required")]
        [StringLength(65535, ErrorMessage = "Parent notes can't be longer than 65535 characters")]
        public string ParentNotes { get; set; }

        [Column("doctor_notes")]
        [StringLength(65535, ErrorMessage = "Doctor notes can't be longer than 65535 characters")]
        public string DoctorNotes { get; set; }

        [JsonIgnore]
        [Column("assignment")]
        [Required(ErrorMessage = "Assignment ID is required")]
        public Guid AssignmentId { get; set; }

        //[JsonIgnore]
        public virtual Assignment Assignment { get; set; }

        [JsonIgnore]
        [Column("parent")]
        [Required(ErrorMessage = "Parent ID is required")]
        public Guid ParentId { get; set; }

        //[JsonIgnore]
        public virtual Parent Parent { get; set; }

        public Session(Guid id, DateTime date, string parentNotes, string doctorNotes, Guid assignmentId, Guid parentId)
        {
            Id = id;
            Date = date;
            ParentNotes = parentNotes;
            DoctorNotes = doctorNotes;
            AssignmentId = assignmentId;
            ParentId = parentId;
        }
    }
}