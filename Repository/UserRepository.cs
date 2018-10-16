using System;
using System.Linq;
using Contracts;
using Entities;
using Entities.Models;
using Entities.ReducedModels;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public User GetUserById(Guid id)
        {
            return FindByCondition((user) => user.Id.Equals((id))).FirstOrDefault();
        }

        public UserReduced GetUserWithoutDetail(Guid id)
        {
            return new UserReduced(GetUserById(id));
        }
    }
}