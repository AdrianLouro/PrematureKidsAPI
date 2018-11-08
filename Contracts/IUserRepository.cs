using System;
using Entities.Models;
using Entities.ReducedModels;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserById(Guid id);

        UserReduced GetUserWithoutDetail(Guid id);

        Guid CreateUser(User user);

        void UpdateUser(User user);

        void DeleteUser(User user);
    }
}