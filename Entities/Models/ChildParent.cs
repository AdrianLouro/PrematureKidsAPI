using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("child_parent")]
    public class ChildParent
    {
        [Column("child")]
        [Required(ErrorMessage = "Child ID is required")]
        public Guid ChildId { get; set; }

        public virtual Child Child { get; set; }

        [Column("parent")]
        [Required(ErrorMessage = "Parent ID is required")]
        public Guid ParentId { get; set; }

        public virtual Parent Parent { get; set; }

        public ChildParent(Guid childId, Guid parentId)
        {
            ChildId = childId;
            ParentId = parentId;
        }
    }
}