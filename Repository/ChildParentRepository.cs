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
    public class ChildParentRepository : RepositoryBase<ChildParent>, IChildParentRepository
    {
        public ChildParentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateChildParent(ChildParent childParent)
        {
            Create(childParent);
            Save();
        }

        public void DeleteChildParent(ChildParent childParent)
        {
            Delete(childParent);
            Save();
        }
    }
}