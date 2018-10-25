using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IChildDoctorRepository : IRepositoryBase<ChildDoctor>
    {
        void CreateChildDoctor(ChildDoctor childDoctor);

        void DeleteChildDoctor(ChildDoctor childDoctor);
    }
}