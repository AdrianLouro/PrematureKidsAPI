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
    public class SessionAttachmentRepository : RepositoryBase<SessionAttachment>, ISessionAttachmentRepository
    {
        public SessionAttachmentRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public SessionAttachmentExtended GetSessionAttachmentById(Guid id)
        {
            return FindByCondition((sessionAttachment) => sessionAttachment.Id.Equals((id)))
                .Select((attachment) => new SessionAttachmentExtended(attachment))
                .FirstOrDefault();
        }

        public IEnumerable<SessionAttachmentExtended> GetSessionAttachments(Guid id)
        {
            return FindByCondition((sessionAttachment) => sessionAttachment.SessionId.Equals((id)))
                .Select((attachment) => new SessionAttachmentExtended(attachment));
        }

        public IEnumerable<SessionAttachmentExtended> GetSessionVideos(Guid id)
        {
            return FindByCondition((sessionAttachment) =>
                sessionAttachment.SessionId.Equals((id)) && sessionAttachment.Type.Equals("video")
            ).Select((attachment) => new SessionAttachmentExtended(attachment));
        }

        public IEnumerable<SessionAttachmentExtended> GetSessionImages(Guid id)
        {
            return FindByCondition((sessionAttachment) =>
                sessionAttachment.SessionId.Equals((id)) && sessionAttachment.Type.Equals("image")
            ).Select((attachment) => new SessionAttachmentExtended(attachment));
        }

        public Guid CreateSessionAttachment(SessionAttachment session)
        {
            //session.Id = Guid.NewGuid();
            Create(session);
            Save();
            return session.Id;
        }

        public void DeleteSessionAttachment(SessionAttachment session)
        {
            Delete(session);
            Save();
        }
    }
}