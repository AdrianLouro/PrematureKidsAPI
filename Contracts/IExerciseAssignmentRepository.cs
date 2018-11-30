using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IExerciseAttachmentRepository : IRepositoryBase<ExerciseAttachment>
    {
        IEnumerable<ExerciseAttachmentExtended> GetExerciseAttachments(Guid id);

        IEnumerable<ExerciseAttachmentExtended> GetExerciseVideos(Guid id);

        IEnumerable<ExerciseAttachmentExtended> GetExerciseImages(Guid id);

        ExerciseAttachmentExtended GetExerciseAttachmentById(Guid id);

        Guid CreateExerciseAttachment(ExerciseAttachment attachment);

        void DeleteExerciseAttachment(ExerciseAttachment attachment);
    }
}