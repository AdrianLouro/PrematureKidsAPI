using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("child_doctor")]
    public class ChildDoctor
    {
        [Column("child")]
        public Guid ChildId { get; set; }
        public virtual Child Child { get; set; }

        [Column("doctor")]
        public Guid DoctorId { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}