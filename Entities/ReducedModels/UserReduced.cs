using System;
using Entities.Models;

namespace Entities.ReducedModels
{
    public class UserReduced
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public string Type { get; set; }

        public UserReduced()
        {
        }

        public UserReduced(User user)
        {
            Id = user.Id;
            Email = user.Email;
            Type = user.Type;
        }
    }
}