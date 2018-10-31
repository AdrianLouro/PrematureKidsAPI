using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface ICategoryRepository : IRepositoryBase<Category>
    {
        IEnumerable<Category> GetAllCategories();
        
        Guid CreateCategory(Category category);

        void UpdateCategory(Category dbCategory, Category category);

        void DeleteCategory(Category category);
    }
}