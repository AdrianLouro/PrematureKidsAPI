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
    [Route("assignments")]
    public class AssignmentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public AssignmentsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet, Authorize]
        public IActionResult GetAllAssignments(Guid categoryId)
        {
            _logger.LogInfo($"Returned all assignments from database.");
            return Ok(_repository.Assignment.GetAllAssignments());
        }

        [HttpGet("{id}", Name = "AssignmentById"), Authorize(Roles = "doctor, parent")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Assignment>))]
        public IActionResult GetAssignmentById(Guid id)
        {
            _logger.LogInfo($"Returned assignment with id: {id}");
            return Ok(_repository.Assignment.GetAssignmentById(id));
        }

        [HttpGet("{id}/sessions"), Authorize(Roles = "doctor, parent")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Assignment>))]
        public IActionResult GetSessions(Guid id)
        {
            _logger.LogInfo($"Returned sessions for id: {id}");
            return Ok(_repository.Session.GetAssignmentSessions(id));
            //return Ok((HttpContext.Items["entity"] as Assignment).Sessions);
        }

        [HttpGet("{id}/exercise"), Authorize(Roles = "doctor, parent")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Assignment>))]
        public IActionResult GetOpinions(Guid id)
        {
            _logger.LogInfo($"Returned exercise for id: {id}");
            return Ok(_repository.Exercise.GetExerciseById((HttpContext.Items["entity"] as Assignment).ExerciseId));
        }

        [HttpPost, Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateAssignment([FromBody] Assignment assignment)
        {
            assignment.Date = DateTime.Now;

            _repository.Assignment.CreateAssignment(assignment);
            return CreatedAtRoute("AssignmentById", new {id = assignment.Id}, assignment);
        }

        [HttpPut("{id}"), Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Assignment>))]
        public IActionResult UpdateAssignment(Guid id, [FromBody] Assignment assignment)
        {
            _repository.Assignment.UpdateAssignment(HttpContext.Items["entity"] as Assignment, assignment);
            return NoContent();
        }

        [HttpDelete("{id}"), Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Assignment>))]
        public IActionResult DeleteAssignment(Guid id)
        {
            _repository.Assignment.DeleteAssignment((HttpContext.Items["entity"] as Assignment));
            return NoContent();
        }
    }
}