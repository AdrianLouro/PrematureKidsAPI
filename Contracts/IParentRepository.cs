﻿using System;
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

        IEnumerable<Child> GetChildrenOfParent(Guid id);

        ParentExtended GetParentWithDetails(Guid id);

        Guid CreateParent(Parent parent);

        void UpdateParent(Parent dbParent, Parent parent);

        void DeleteParent(Parent parent);
    }
}