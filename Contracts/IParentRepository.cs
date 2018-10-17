using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IParentRepository : IRepositoryBase<Parent>
    {
        IEnumerable<Parent> GetAllParents();

        Parent GetParentById(Guid id);

        ParentExtended GetParentWithDetails(Guid id);

        void CreateParent(Parent parent);
    }
}