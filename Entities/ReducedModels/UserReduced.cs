using System;
using Entities.Models;

namespace Entities.ReducedModels
{
    public class UserReduced: IEntity
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }

        public UserReduced()
        {
        }

        public UserReduced(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Role = user.Role;
        }
    }
}