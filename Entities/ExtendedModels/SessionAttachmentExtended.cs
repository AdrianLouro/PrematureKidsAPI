using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class SessionAttachmentExtended : IEntity
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public string Type { get; set; }

        [Required(ErrorMessage = "Session ID is required")]
        public Guid SessionId { get; set; }

        public string FullPath { get; set; }

        public SessionAttachmentExtended(SessionAttachment sessionAttachment)
        {
            Id = sessionAttachment.Id;
            Name = sessionAttachment.Name;
            Type = sessionAttachment.Type;
            SessionId = sessionAttachment.SessionId;
            FullPath = "http://192.168.1.10:5000/uploads/sessions/" + Id;
        }
    }
}