using Entities.Models;

namespace Entities.Extensions
{
    public static class AssignmentExtensions
    {
        public static void Map(this Assignment dbAssignment, Assignment assignment)
        {
            dbAssignment.Notes = assignment.Notes;
            dbAssignment.ExerciseFrequency = assignment.ExerciseFrequency;
            dbAssignment.ExerciseDuration = assignment.ExerciseDuration;
            dbAssignment.FeedbackFrequency = assignment.FeedbackFrequency;
            dbAssignment.State = assignment.State;
        }
    }
}