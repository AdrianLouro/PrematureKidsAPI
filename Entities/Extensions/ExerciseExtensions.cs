using Entities.Models;

namespace Entities.Extensions
{
    public static class ExerciseExtensions
    {
        public static void Map(this Exercise dbExercise, Exercise exercise)
        {
            dbExercise.Title = exercise.Title;
            dbExercise.CategoryId = exercise.CategoryId;
        }
    }
}