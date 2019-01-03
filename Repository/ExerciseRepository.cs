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
    public class ExerciseRepository : RepositoryBase<Exercise>, IExerciseRepository
    {
        public ExerciseRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Exercise> GetAllExercises()
        {
            return this.RepositoryContext.Set<Exercise>()
                .Include(exercise => exercise.Doctor)
                .Include(exercise => exercise.Category)
                .OrderBy(exercise => exercise.Title);
        }

        public IEnumerable<Exercise> GetDoctorExercises(Guid id)
        {
            return GetAllExercises().Where(exercise => exercise.DoctorId == id);
        }

        public IEnumerable<Exercise> GetCategoryExercises(Guid id)
        {
            return GetAllExercises().Where(exercise => exercise.CategoryId == id);
        }

        public Exercise GetExerciseById(Guid id)
        {
            return GetAllExercises().FirstOrDefault(exercise => exercise.Id.Equals((id)));
        }

        public Guid CreateExercise(Exercise exercise)
        {
            exercise.Id = Guid.NewGuid();
            Create(exercise);
            Save();
            return exercise.Id;
        }

        public void UpdateExercise(Exercise dbExercise, Exercise exercise)
        {
            dbExercise.Map(exercise);
            Update(dbExercise);
            Save();
        }

        public void DeleteExercise(Exercise exercise)
        {
            Delete(exercise);
            Save();
        }
    }
}