using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class AdministratorRepository : RepositoryBase<Administrator>, IAdministratorRepository
    {
        public AdministratorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public Administrator GetAdministratorById(Guid id)
        {
            return FindByCondition(child => child.Id.Equals((id))).FirstOrDefault();
        }

        public void UpdateAdministrator(Administrator dbAdministrator, Administrator child)
        {
            dbAdministrator.Map(child);
            Update(dbAdministrator);
            Save();
        }

    }
}