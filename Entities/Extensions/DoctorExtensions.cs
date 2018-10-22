using Entities.Models;

namespace Entities.Extensions
{
    public static class DoctorExtensions
    {
        public static void Map(this Doctor dbDoctor, Doctor doctor)
        {
            dbDoctor.Name = doctor.Name;
            dbDoctor.BoardNumber = doctor.BoardNumber;
            dbDoctor.Telephone = doctor.Telephone;
        }
    }
}