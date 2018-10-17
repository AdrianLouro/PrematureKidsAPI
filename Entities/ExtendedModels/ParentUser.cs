using System;
using Entities.Models;
using Newtonsoft.Json;

namespace Entities.ExtendedModels
{
    public class ParentUser
    {
        public Guid ParentId { get; set; }

        public Guid UserId { get; set; }

        public string Name { get; set; }

        public string IdNumber { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Role = "parent";
    }
}