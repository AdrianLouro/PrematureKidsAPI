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

        [HttpGet]
        public IActionResult GetAllDoctors()
        {
            _logger.LogInfo($"Returned all doctors from database.");
            return Ok(_repository.Doctor.GetAllDoctors());
        }

        [HttpGet("{id}", Name = "DoctorById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetDoctorById(Guid id)
        {
            _logger.LogInfo($"Returned doctorUser with id: {id}");
            return Ok(HttpContext.Items["entity"] as Doctor);
        }

        [HttpGet("{id}/user")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            _logger.LogInfo($"Returned user without details for id: {id}");
            return Ok(_repository.User.GetUserWithoutDetail((HttpContext.Items["entity"] as Doctor).Id));
        }

        [HttpGet("{id}/patients")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult GetPatients(Guid id)
        {
            _logger.LogInfo($"Returned patients for id: {id}");
            return Ok(_repository.Doctor.GetPatientsOfDoctor(id));
            //return Ok((HttpContext.Items["entity"] as Doctor).Patients);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateDoctor([FromBody] DoctorUser doctorUser)
        {
            var userId = _repository.User.CreateUser(new User(
                doctorUser.Id,
                doctorUser.Email,
                Crypto.HashPassword(doctorUser.Password),
                doctorUser.Role
            ));

            var doctor = new Doctor(
                userId,
                doctorUser.Name,
                doctorUser.BoardNumber,
                doctorUser.Telephone
            );

            _repository.Doctor.CreateDoctor(doctor);
            return CreatedAtRoute("DoctorById", new {id = doctorUser.Id}, doctor);
        }


        [HttpPut("{id}"), Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Doctor>))]
        public IActionResult UpdateDoctor(Guid id, [FromBody] Doctor doctor)
        {
            _repository.Doctor.UpdateDoctor(HttpContext.Items["entity"] as Doctor, doctor);
            return NoContent();
        }

        [HttpDelete("{id}")]
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