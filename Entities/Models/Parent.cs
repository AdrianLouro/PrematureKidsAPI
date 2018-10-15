using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("parent")]
    public class Parent
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "ID number is required")]
        [StringLength(255, ErrorMessage = "ID number can't be longer than 255 characters")]
        public string IdNumber { get; set; }

        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(255, ErrorMessage = "Telephone cannot be longer then 255 characters")]
        public string Telephone { get; set; }
    }
}
