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
    [Route("exercisesAttachments")]
    public class ExercisesAttachmentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ExercisesAttachmentsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}", Name = "ExerciseAttachmentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ExerciseAttachment>))]
        public IActionResult GetExerciseAttachmentById(Guid id)
        {
            _logger.LogInfo($"Returned exercise attachment with id: {id}");
            return Ok(_repository.ExerciseAttachment.GetExerciseAttachmentById(id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateExerciseAttachment([FromBody] ExerciseAttachment exerciseAttachment)
        {
            _repository.ExerciseAttachment.CreateExerciseAttachment(exerciseAttachment);
            return CreatedAtRoute("ExerciseAttachmentById", new {id = exerciseAttachment.Id}, new ExerciseAttachmentExtended(exerciseAttachment));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ExerciseAttachment>))]
        public IActionResult DeleteExerciseAttachment(Guid id)
        {
            _repository.ExerciseAttachment.DeleteExerciseAttachment(
                (HttpContext.Items["entity"] as ExerciseAttachment));
            return NoContent();
        }
    }
}