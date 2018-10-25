using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IChildParentRepository : IRepositoryBase<ChildParent>
    {
        void CreateChildParent(ChildParent childParent);

        void DeleteChildParent(ChildParent childParent);
    }
}