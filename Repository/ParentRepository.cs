using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Models;

namespace Repository
{
    public class ParentRepository : RepositoryBase<Parent>, IParentRepository
    {
        public ParentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Parent> GetAllParents()
        {
            return FindAll().OrderBy(parent => parent.Name);
        }

        public Parent GetParentById(Guid id)
        {
            return FindByCondition((parent) => parent.Id.Equals((id))).FirstOrDefault();
        }

        public ParentExtended GetParentWithDetails(Guid id)
        {
            var parent = GetParentById(id);
            return new ParentExtended(parent)
            {
                Email = RepositoryContext.Users.Where((user) => user.Id == parent.Id).FirstOrDefault().Email
            };
        }

        public void CreateParent(Parent parent)
        {
            parent.Id = Guid.NewGuid();
            Create(parent);
            Save();
        }
    }
}