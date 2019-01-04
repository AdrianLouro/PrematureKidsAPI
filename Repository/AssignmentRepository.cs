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
    public class AssignmentRepository : RepositoryBase<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Assignment> GetAllAssignments()
        {
            return this.RepositoryContext.Set<Assignment>()
                .Include(assignment => assignment.Doctor)
                .Include(assignment => assignment.Child)
                .Include(assignment => assignment.Exercise)
                .ThenInclude(exercise => exercise.Category)
                .OrderByDescending(assignment => assignment.Date);
        }

        public IEnumerable<Assignment> GetChildAssignments(Guid id)
        {
            return GetAllAssignments().Where(assignment => assignment.ChildId == id);
        }

        public Assignment GetAssignmentById(Guid id)
        {
            return GetAllAssignments().FirstOrDefault(assignment => assignment.Id.Equals((id)));
        }

        public Guid CreateAssignment(Assignment assignment)
        {
            assignment.Id = Guid.NewGuid();
            assignment.Date = DateTime.Now;
            Create(assignment);
            Save();
            return assignment.Id;
        }

        public void UpdateAssignment(Assignment dbAssignment, Assignment assignment)
        {
            dbAssignment.Map(assignment);
            Update(dbAssignment);
            Save();
        }

        public void DeleteAssignment(Assignment assignment)
        {
            Delete(assignment);
            Save();
        }
    }
}