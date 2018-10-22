using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    [Table("child_parent")]
    public class ChildParent
    {
        [Column("child")]
        public Guid ChildId { get; set; }
        public virtual Child Child { get; set; }

        [Column("parent")]
        public Guid ParentId { get; set; }
        public virtual Parent Parent { get; set; }
    }
}