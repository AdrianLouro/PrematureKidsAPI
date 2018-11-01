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
    [Route("opinions")]
    public class OpinionsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public OpinionsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllOpinions(Guid exerciseId, Guid parentId)
        {
            _logger.LogInfo($"Returned all opinions from database.");
            return Ok(
                exerciseId != Guid.Empty && parentId != Guid.Empty
                    ? _repository.Opinion.FindByCondition(opinion =>
                        opinion.ExerciseId == exerciseId && opinion.ParentId == parentId
                    )
                    : _repository.Opinion.GetAllOpinions()
            );
        }

        [HttpGet("{id}", Name = "OpinionById")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Opinion>))]
        public IActionResult GetOpinionById(Guid id)
        {
            _logger.LogInfo($"Returned opinion with id: {id}");
            return Ok(_repository.Opinion.GetOpinionById(id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateOpinion([FromBody] Opinion opinion)
        {
            _repository.Opinion.CreateOpinion(opinion);
            return CreatedAtRoute("OpinionById", new {id = opinion.Id}, opinion);
        }

        [HttpPut("{id}"), Authorize(Roles = "parent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Opinion>))]
        public IActionResult UpdateOpinion(Guid id, [FromBody] Opinion opinion)
        {
            _repository.Opinion.UpdateOpinion(HttpContext.Items["entity"] as Opinion, opinion);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Opinion>))]
        public IActionResult DeleteOpinion(Guid id)
        {
            _repository.Opinion.DeleteOpinion((HttpContext.Items["entity"] as Opinion));
            return NoContent();
        }
    }
}