using System;
using System.Collections.Generic;
using System.Linq;
using ActionFilters;
using Contracts;
using CryptoHelper;
using Entities.ExtendedModels;
using Entities.Models;
using Entities.ReducedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PrematureKidsAPI.Controllers
{
    [Route("doctors")]
    public class DoctorsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public DoctorsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet, Authorize(Roles = "administrator")]
        public IActionResult GetAllDoctors()
        {
            _logger.LogInfo($"Returned all doctors from database.");
            return Ok(_repository.Doctor.GetAllDoctors());
        }

        [HttpGet("{id}", Name = "DoctorById"), Authorize]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetDoctorById(Guid id)
        {
            _logger.LogInfo($"Returned doctor with id: {id}");
            return Ok(HttpContext.Items["entity"] as Doctor);
        }

        [HttpGet("{id}/user"), Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            _logger.LogInfo($"Returned user without details for id: {id}");
            return Ok(_repository.User.GetUserWithoutDetail((HttpContext.Items["entity"] as Doctor).Id));
        }

        [HttpGet("{id}/patients"), Authorize(Roles = "doctor")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetPatients(Guid id)
        {
            _logger.LogInfo($"Returned patients for id: {id}");
            return Ok(_repository.Doctor.GetPatientsOfDoctor(id));
            //return Ok((HttpContext.Items["entity"] as Doctor).Patients);
        }

        [HttpGet("{id}/parents"), Authorize(Roles = "doctor")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetParents(Guid id)
        {
            _logger.LogInfo($"Returned parents for id: {id}");
            return Ok(_repository.Doctor.GetParentsOfDoctor(id));
        }

        [HttpGet("{id}/exercises"), Authorize(Roles = "doctor")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GerExercises(Guid id)
        {
            _logger.LogInfo($"Returned exercises for id: {id}");
            return Ok(_repository.Exercise.GetDoctorExercises(id));
        }

        [HttpPost("{id}/patients/{patientId}"), Authorize(Roles = "doctor")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddPatient(Guid id, Guid patientId)
        {
            if (_repository.ChildDoctor.FindByCondition(
                childDoctor => childDoctor.ChildId.Equals(patientId) && childDoctor.DoctorId.Equals(id)
            ).Any())
                return Conflict("The patient is already associated");

            _repository.ChildDoctor.CreateChildDoctor(new ChildDoctor(patientId, id));
            return NoContent();
        }

        [HttpPost, Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateDoctor([FromBody] DoctorUser doctorUser)
        {
            if (_repository.User.FindByCondition(user => user.Email.Equals(doctorUser.Email)).Any())
                return Conflict("email");

            if (_repository.Doctor.FindByCondition(d => d.BoardNumber.Equals(doctorUser.BoardNumber)).Any())
                return Conflict("boardNumber");

            Guid userId = _repository.User.CreateUser(new User(
                doctorUser.Id,
                doctorUser.Email,
                Crypto.HashPassword("password"),
                doctorUser.Role
            ));

            Doctor doctor = new Doctor(
                userId,
                doctorUser.Name,
                doctorUser.BoardNumber,
                doctorUser.Telephone
            );

            _repository.Doctor.CreateDoctor(doctor);
            return CreatedAtRoute("DoctorById", new {id = doctorUser.Id}, doctor);
        }

        [HttpPut("{id}"), Authorize(Roles = "doctor, administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult UpdateDoctor(Guid id, [FromBody] Doctor doctor)
        {
            if (_repository.Doctor.FindByCondition(
                d => d.BoardNumber.Equals(doctor.BoardNumber) && !d.Id.Equals(id)
            ).Any())
                return Conflict("boardNumber");

            _repository.Doctor.UpdateDoctor(HttpContext.Items["entity"] as Doctor, doctor);
            return NoContent();
        }

        [HttpDelete("{id}"), Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult DeleteDoctor(Guid id)
        {
            _repository.User.DeleteUser(
                _repository.User
                    .FindByCondition((user => user.Id == (HttpContext.Items["entity"] as Doctor).Id))
                    .FirstOrDefault()
            );
            return NoContent();
        }
    }
}