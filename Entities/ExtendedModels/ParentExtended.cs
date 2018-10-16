using System;
using System.Dynamic;
using Entities.Models;

namespace Entities.ExtendedModels
{
    public class ParentExtended
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string IdNumber { get; set; }

        public string Telephone { get; set; }

        public string Email { get; set; }

        public ParentExtended()
        {
        }

        public ParentExtended(Parent parent)
        {
            Id = parent.Id;
            Name = parent.Name;
            IdNumber = parent.IdNumber;
            Telephone = parent.Telephone;
        }
    }
}