using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("child_doctor")]
    public class ChildDoctor
    {
        [Column("child")]
        [Required(ErrorMessage = "Child ID is required")]
        public Guid ChildId { get; set; }

        public virtual Child Child { get; set; }

        [Column("doctor")]
        [Required(ErrorMessage = "Doctor ID is required")]
        public Guid DoctorId { get; set; }

        public virtual Doctor Doctor { get; set; }

        public ChildDoctor(Guid childId, Guid doctorId)
        {
            ChildId = childId;
            DoctorId = doctorId;
        }
    }
}