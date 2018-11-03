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
    public class SessionRepository : RepositoryBase<Session>, ISessionRepository
    {
        public SessionRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Session> GetAllSessions()
        {
            return this.RepositoryContext.Set<Session>()
                .Include(session => session.Parent)
                .Include(session => session.Assignment)
                .ThenInclude(assignment => assignment.Doctor)
                .OrderByDescending(session => session.Date);
        }

        public IEnumerable<Session> GetAssignmentSessions(Guid id)
        {
            return GetAllSessions().Where(session => session.AssignmentId == id);
        }

        public Session GetSessionById(Guid id)
        {
            return GetAllSessions().FirstOrDefault(session => session.Id.Equals((id)));
        }

        public Guid CreateSession(Session session)
        {
            session.Id = Guid.NewGuid();
            Create(session);
            Save();
            return session.Id;
        }

        public void UpdateSession(Session dbSession, Session session)
        {
            dbSession.Map(session);
            Update(dbSession);
            Save();
        }

        public void DeleteSession(Session session)
        {
            Delete(session);
            Save();
        }
    }
}