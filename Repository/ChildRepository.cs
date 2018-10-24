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
    public class ChildRepository : RepositoryBase<Child>, IChildRepository
    {
        public ChildRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Child> GetAllChildren()
        {
            return FindAll().OrderBy(child => child.Name);
        }

        public Child GetChildById(Guid id)
        {
            return FindByCondition((child) => child.Id.Equals((id))).FirstOrDefault();
        }

        public IEnumerable<Parent> GetParentsOfChild(Guid id)
        {
            return this.RepositoryContext.Set<Child>()
                .Include(c => c.Parents)
                .ThenInclude(cp => cp.Parent)
                .SingleOrDefault(x => x.Id.Equals(id)).Parents.Select(cp => cp.Parent);
        }

        public IEnumerable<Doctor> GetDoctorsOfChild(Guid id)
        {
            return this.RepositoryContext.Set<Child>()
                .Include(c => c.Doctors)
                .ThenInclude(cd => cd.Doctor)
                .SingleOrDefault(x => x.Id.Equals(id)).Doctors.Select(cd => cd.Doctor);
        }

        public Guid CreateChild(Child child)
        {
            child.Id = Guid.NewGuid();
            Create(child);
            Save();
            return child.Id;
        }

        public void UpdateChild(Child dbChild, Child child)
        {
            dbChild.Map(child);
            Update(dbChild);
            Save();
        }

        public void DeleteChild(Child child)
        {
            Delete(child);
            Save();
        }
    }
}