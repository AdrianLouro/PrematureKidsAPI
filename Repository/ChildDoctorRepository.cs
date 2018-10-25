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
    public class ChildDoctorRepository : RepositoryBase<ChildDoctor>, IChildDoctorRepository
    {
        public ChildDoctorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateChildDoctor(ChildDoctor childDoctor)
        {
            Create(childDoctor);
            Save();
        }

        public void DeleteChildDoctor(ChildDoctor childDoctor)
        {
            Delete(childDoctor);
            Save();
        }
    }
}