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
    public class CategoryRepository : RepositoryBase<Category>, ICategoryRepository
    {
        public CategoryRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return FindAll().OrderBy(category => category.Name);
        }

        public Guid CreateCategory(Category category)
        {
            category.Id = Guid.NewGuid();
            Create(category);
            Save();
            return category.Id;
        }

        public void UpdateCategory(Category dbCategory, Category category)
        {
            dbCategory.Map(category);
            Update(dbCategory);
            Save();
        }

        public void DeleteCategory(Category category)
        {
            Delete(category);
            Save();
        }
    }
}