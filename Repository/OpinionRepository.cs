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
    public class OpinionRepository : RepositoryBase<Opinion>, IOpinionRepository
    {
        public OpinionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Opinion> GetAllOpinions()
        {
            return this.RepositoryContext.Set<Opinion>()
                .Include(opinion => opinion.Exercise)
                .Include(opinion => opinion.Parent)
                .OrderBy(opinion => opinion.Parent.Name);
        }

        public IEnumerable<Opinion> GetExerciseOpinions(Guid id)
        {
            return GetAllOpinions().Where(opinion => opinion.ExerciseId == id);
        }

        public Opinion GetOpinionById(Guid id)
        {
            return GetAllOpinions().FirstOrDefault(opinion => opinion.Id.Equals((id)));
        }

        public Guid CreateOpinion(Opinion opinion)
        {
            opinion.Id = Guid.NewGuid();
            Create(opinion);
            Save();
            return opinion.Id;
        }

        public void UpdateOpinion(Opinion dbOpinion, Opinion opinion)
        {
            dbOpinion.Map(opinion);
            Update(dbOpinion);
            Save();
        }

        public void DeleteOpinion(Opinion opinion)
        {
            Delete(opinion);
            Save();
        }
    }
}