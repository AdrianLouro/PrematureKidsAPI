using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface ISessionAttachmentRepository : IRepositoryBase<SessionAttachment>
    {

        IEnumerable<SessionAttachmentExtended> GetSessionAttachments(Guid id);

        IEnumerable<SessionAttachmentExtended> GetSessionVideos(Guid id);

        IEnumerable<SessionAttachmentExtended> GetSessionImages(Guid id);

        SessionAttachmentExtended GetSessionAttachmentById(Guid id);

        Guid CreateSessionAttachment(SessionAttachment attachment);

        void DeleteSessionAttachment(SessionAttachment attachment);
    }
}