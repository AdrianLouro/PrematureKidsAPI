using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Models
{
    [Table("doctor")]
    public class Doctor : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Column("board_number")]
        [Required(ErrorMessage = "Board number is required")]
        [StringLength(255, ErrorMessage = "Board number can't be longer than 255 characters")]
        public string BoardNumber { get; set; }

        [Column("telephone")]
        [Required(ErrorMessage = "Telephone is required")]
        [StringLength(255, ErrorMessage = "Telephone cannot be longer then 255 characters")]
        public string Telephone { get; set; }

        [Column("user_id")]
        [Required(ErrorMessage = "User ID is required")]
        public Guid UserId { get; set; }

        public Doctor(Guid id, string name, string boardNumber, string telephone, Guid userId)
        {
            Id = id;
            Name = name;
            BoardNumber = boardNumber;
            Telephone = telephone;
            UserId = userId;
        }
    }
}