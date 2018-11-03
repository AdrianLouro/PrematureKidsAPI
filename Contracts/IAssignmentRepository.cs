using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IAssignmentRepository : IRepositoryBase<Assignment>
    {
        IEnumerable<Assignment> GetAllAssignments();

        IEnumerable<Assignment> GetChildAssignments(Guid id);

        Assignment GetAssignmentById(Guid id);

        Guid CreateAssignment(Assignment assignment);

        void UpdateAssignment(Assignment dbAssignment, Assignment assignment);

        void DeleteAssignment(Assignment assignment);
    }
}