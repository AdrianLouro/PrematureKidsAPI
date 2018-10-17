using System;
using System.Collections.Generic;
using System.Linq;
using Contracts;
using Entities;
using Entities.ExtendedModels;
using Entities.Models;

namespace Repository
{
    public class DoctorRepository : RepositoryBase<Doctor>, IDoctorRepository
    {
        public DoctorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Doctor> GetAllDoctors()
        {
            return FindAll().OrderBy(doctor => doctor.Name);
        }

        public Doctor GetDoctorById(Guid id)
        {
            return FindByCondition((doctor) => doctor.Id.Equals((id))).FirstOrDefault();
        }

        public DoctorExtended GetDoctorWithDetails(Guid id)
        {
            var doctor = GetDoctorById(id);
            return new DoctorExtended(doctor)
            {
                Email = RepositoryContext.Users.Where((user) => user.Id == doctor.Id).FirstOrDefault().Email
            };
        }

        public void CreateDoctor(Doctor doctor)
        {
            doctor.Id = Guid.NewGuid();
            Create(doctor);
            Save();
        }
    }
}