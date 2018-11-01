using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IOpinionRepository : IRepositoryBase<Opinion>
    {
        IEnumerable<Opinion> GetAllOpinions();

        IEnumerable<Opinion> GetExerciseOpinions(Guid id);

        Opinion GetOpinionById(Guid id);

        Guid CreateOpinion(Opinion opinion);

        void UpdateOpinion(Opinion dbOpinion, Opinion opinion);

        void DeleteOpinion(Opinion opinion);
    }
}