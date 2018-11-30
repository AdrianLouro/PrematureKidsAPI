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
    [Route("exercises")]
    public class ExercisesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ExercisesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllExercises(Guid categoryId)
        {
            _logger.LogInfo($"Returned all exercises from database.");
            return Ok(
                categoryId == Guid.Empty
                    ? _repository.Exercise.GetAllExercises()
                    : _repository.Exercise.GetCategoryExercises(categoryId)
            );
        }

        [HttpGet("{id}", Name = "ExerciseById")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult GetExerciseById(Guid id)
        {
            _logger.LogInfo($"Returned exercise with id: {id}");
            return Ok(_repository.Exercise.GetExerciseById(id));
        }

        [HttpGet("{id}/opinions")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult GetOpinions(Guid id)
        {
            _logger.LogInfo($"Returned opinions for id: {id}");
            return Ok(_repository.Opinion.GetExerciseOpinions((HttpContext.Items["entity"] as Exercise).Id));
        }

        [HttpGet("{id}/videos")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult GetVideos(Guid id)
        {
            _logger.LogInfo($"Returned videos for id: {id}");
            return Ok(_repository.ExerciseAttachment.GetExerciseVideos((HttpContext.Items["entity"] as Exercise).Id));
        }

        [HttpGet("{id}/images")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult GetImages(Guid id)
        {
            _logger.LogInfo($"Returned images for id: {id}");
            return Ok(_repository.ExerciseAttachment.GetExerciseImages((HttpContext.Items["entity"] as Exercise).Id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateExercise([FromBody] Exercise exercise)
        {
            _repository.Exercise.CreateExercise(exercise);
            return CreatedAtRoute("ExerciseById", new {id = exercise.Id}, exercise);
        }

        [HttpPut("{id}"), Authorize(Roles = "doctor")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult UpdateExercise(Guid id, [FromBody] Exercise exercise)
        {
            _repository.Exercise.UpdateExercise(HttpContext.Items["entity"] as Exercise, exercise);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Exercise>))]
        public IActionResult DeleteExercise(Guid id)
        {
            _repository.Exercise.DeleteExercise((HttpContext.Items["entity"] as Exercise));
            return NoContent();
        }
    }
}