using Entities.Models;

namespace Entities.Extensions
{
    public static class ExerciseExtensions
    {
        public static void Map(this Exercise dbExercise, Exercise exercise)
        {
            dbExercise.Title = exercise.Title;
            dbExercise.Description = exercise.Description;
            dbExercise.CategoryId = exercise.CategoryId;
        }
    }
}