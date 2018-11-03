using Entities.Models;

namespace Entities.Extensions
{
    public static class SessionExtensions
    {
        public static void Map(this Session dbSession, Session session)
        {
            dbSession.Date = session.Date;
            dbSession.ParentNotes = session.ParentNotes;
            dbSession.DoctorNotes = session.DoctorNotes;
        }
    }
}