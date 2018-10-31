using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IExerciseRepository : IRepositoryBase<Exercise>
    {
        IEnumerable<Exercise> GetAllExercises();

        IEnumerable<Exercise> GetDoctorExercises(Guid id);

        IEnumerable<Exercise> GetCategoryExercises(Guid id);

        Exercise GetExerciseById(Guid id);

        Guid CreateExercise(Exercise exercise);

        void UpdateExercise(Exercise dbExercise, Exercise exercise);

        void DeleteExercise(Exercise exercise);
    }
}