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

        public IEnumerable<Child> GetPatientsOfDoctor(Guid id)
        {
            return this.RepositoryContext.Set<Doctor>()
                .Include(d => d.Patients)
                .ThenInclude(cd => cd.Child)
                .SingleOrDefault(x => x.Id.Equals(id)).Patients.Select(cd => cd.Child);
        }

        public IEnumerable<Parent> GetParentsOfDoctor(Guid id)
        {
            return this.RepositoryContext.Set<Doctor>()
                .Include(d => d.Patients)
                .ThenInclude(cd => cd.Child)
                .ThenInclude(c => c.Parents)
                .ThenInclude(cp => cp.Parent)
                .SingleOrDefault(x => x.Id.Equals(id)).Patients.Select(cd => cd.Child)
                .Select(c => c.Parents.Select(cp => cp.Parent)).SelectMany(x => x).Distinct();
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
            //doctor.Id = Guid.NewGuid();
            Create(doctor);
            Save();
        }

        public void UpdateDoctor(Doctor dbDoctor, Doctor doctor)
        {
            dbDoctor.Map(doctor);
            Update(dbDoctor);
            Save();
        }

        public void DeleteDoctor(Doctor doctor)
        {
            Delete(doctor);
            Save();
        }
    }
}