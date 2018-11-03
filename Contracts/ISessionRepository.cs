using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface ISessionRepository : IRepositoryBase<Session>
    {
        IEnumerable<Session> GetAllSessions();

        IEnumerable<Session> GetAssignmentSessions(Guid id);

        Session GetSessionById(Guid id);

        Guid CreateSession(Session session);

        void UpdateSession(Session dbSession, Session session);

        void DeleteSession(Session session);
    }
}