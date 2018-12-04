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
    public class ExerciseAttachmentRepository : RepositoryBase<ExerciseAttachment>, IExerciseAttachmentRepository
    {
        public ExerciseAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public ExerciseAttachmentExtended GetExerciseAttachmentById(Guid id)
        {
            return FindByCondition((exerciseAttachment) => exerciseAttachment.Id.Equals((id)))
                .Select((attachment) => new ExerciseAttachmentExtended(attachment))
                .FirstOrDefault();
        }

        public IEnumerable<ExerciseAttachmentExtended> GetExerciseAttachments(Guid id)
        {
            return FindByCondition((exerciseAttachment) => exerciseAttachment.ExerciseId.Equals((id)))
                .Select((attachment) => new ExerciseAttachmentExtended(attachment));
        }

        public IEnumerable<ExerciseAttachmentExtended> GetExerciseVideos(Guid id)
        {
            return FindByCondition((exerciseAttachment) =>
                exerciseAttachment.ExerciseId.Equals((id)) && exerciseAttachment.Type.Equals("video")
            ).Select((attachment) => new ExerciseAttachmentExtended(attachment));
        }

        public IEnumerable<ExerciseAttachmentExtended> GetExerciseImages(Guid id)
        {
            return FindByCondition((exerciseAttachment) =>
                exerciseAttachment.ExerciseId.Equals((id)) && exerciseAttachment.Type.Equals("image")
            ).Select((attachment) => new ExerciseAttachmentExtended(attachment));
        }

        public Guid CreateExerciseAttachment(ExerciseAttachment exercise)
        {
            //exercise.Id = Guid.NewGuid();
            Create(exercise);
            Save();
            return exercise.Id;
        }

        public void DeleteExerciseAttachment(ExerciseAttachment exercise)
        {
            Delete(exercise);
            Save();
        }
    }
}