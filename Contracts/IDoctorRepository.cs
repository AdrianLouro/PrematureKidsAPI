using System;
using System.Collections;
using System.Collections.Generic;
using Entities.ExtendedModels;
using Entities.Models;

namespace Contracts
{
    public interface IDoctorRepository : IRepositoryBase<Doctor>
    {
        IEnumerable<Doctor> GetAllDoctors();

        Doctor GetDoctorById(Guid id);

        DoctorExtended GetDoctorWithDetails(Guid id);

        IEnumerable<Child> GetPatientsOfDoctor(Guid id);

        void CreateDoctor(Doctor doctor);

        void UpdateDoctor(Doctor dbDoctor, Doctor doctor);

        void DeleteDoctor(Doctor doctor);
    }
}