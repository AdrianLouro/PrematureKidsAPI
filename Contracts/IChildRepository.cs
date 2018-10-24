using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IChildRepository : IRepositoryBase<Child>
    {
        IEnumerable<Child> GetAllChildren();
        
        Child GetChildById(Guid id);

        IEnumerable<Parent> GetParentsOfChild(Guid id);

        IEnumerable<Doctor> GetDoctorsOfChild(Guid id);

        Guid CreateChild(Child child);

        void UpdateChild(Child dbChild, Child child);

        void DeleteChild(Child child);
    }
}