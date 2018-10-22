using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("parent")]
    public class Parent : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Column("id_number")]
        [Required(ErrorMessage = "ID number is required")]
        [StringLength(255, ErrorMessage = "ID number can't be longer than 255 characters")]
        public string IdNumber { get; set; }

        [Column("telephone")]
        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(255, ErrorMessage = "Telephone cannot be longer then 255 characters")]
        public string Telephone { get; set; }

        [JsonIgnore]
        public virtual ICollection<ChildParent> Children { get; set; } = new List<ChildParent>();

        public Parent(Guid id, string name, string idNumber, string telephone)
        {
            Id = id;
            Name = name;
            IdNumber = idNumber;
            Telephone = telephone;
        }
    }
}