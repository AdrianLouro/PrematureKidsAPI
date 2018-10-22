﻿using System;
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

        public IEnumerable<Child> GetChildrenOfParent(Guid id)
        {
            return this.RepositoryContext.Set<Parent>()
                .Include(p => p.Children)
                .ThenInclude(cp => cp.Child)
                .SingleOrDefault(x => x.Id.Equals(id)).Children.Select(cp => new Child(
                    cp.Child.Id,
                    cp.Child.Name,
                    cp.Child.DateOfBirth,
                    cp.Child.Gender
                ));
        }

        public ParentExtended GetParentWithDetails(Guid id)
        {
            var parent = GetParentById(id);
            return new ParentExtended(parent)
            {
                Email = RepositoryContext.Users.Where((user) => user.Id == parent.Id).FirstOrDefault().Email
            };
        }

        public Guid CreateParent(Parent parent)
        {
            //parent.Id = Guid.NewGuid();
            Create(parent);
            Save();
            return parent.Id;
        }

        public void UpdateParent(Parent dbParent, Parent parent)
        {
            dbParent.Map(parent);
            Update(dbParent);
            Save();
        }

        public void DeleteParent(Parent parent)
        {
            Delete(parent);
            Save();
        }
    }
}