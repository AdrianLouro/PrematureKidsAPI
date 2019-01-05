using System;
using Entities.Models;

namespace Entities.ReducedModels
{
    public class UserStatus : IEntity
    {
        public Guid Id { get; set; }

        public bool Blocked { get; set; }
    }
}