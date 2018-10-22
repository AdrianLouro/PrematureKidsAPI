using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Extensions;
using Entities.Models;

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