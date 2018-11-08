using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IAdministratorRepository : IRepositoryBase<Administrator>
    {
        Administrator GetAdministratorById(Guid id);

        void UpdateAdministrator(Administrator dbAdministrator, Administrator administrator);
    }
}