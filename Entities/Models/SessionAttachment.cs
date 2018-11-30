using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Newtonsoft.Json;

namespace Entities.Models
{
    [Table("session_attachment")]
    public class SessionAttachment : IEntity
    {
        [Key] [Column("id")] public Guid Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(255, ErrorMessage = "Name can't be longer than 255 characters")]
        public string Name { get; set; }

        [Column("type")]
        [Required(ErrorMessage = "Type is required")]
        [StringLength(255, ErrorMessage = "Type can't be longer than 255 characters")]
        public string Type { get; set; }

        [JsonIgnore]
        [Column("session")]
        [Required(ErrorMessage = "Session ID is required")]
        public Guid SessionId { get; set; }

        [JsonIgnore]
        public virtual Session Session { get; set; }

        public SessionAttachment(Guid id, string name, string type, Guid sessionId)
        {
            Id = id;
            Name = name;
            Type = type;
            SessionId = sessionId;
        }
    }
}