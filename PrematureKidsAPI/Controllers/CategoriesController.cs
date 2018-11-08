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
    [Route("categories")]
    public class CategoriesController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public CategoriesController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllCategories()
        {
            _logger.LogInfo($"Returned all categories.");
            return Ok(_repository.Category.GetAllCategories());
        }

        [HttpGet("{id}", Name = "CategoryById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult GetCategoryById(Guid id)
        {
            _logger.LogInfo($"Returned category with id: {id}");
            return Ok(_repository.Category.GetCategoryById(id));
        }

        [HttpGet("{id}/exercises")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult GerExercises(Guid id)
        {
            _logger.LogInfo($"Returned exercises for id: {id}");
            return Ok(_repository.Exercise.GetCategoryExercises(id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateCategory([FromBody] Category category)
        {
            _repository.Category.CreateCategory(category);
            return CreatedAtRoute("CategoryById", new {id = category.Id}, category);
        }

        [HttpPut("{id}"), Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult UpdateCategory(Guid id, [FromBody] Category category)
        {
            _repository.Category.UpdateCategory(HttpContext.Items["entity"] as Category, category);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult DeleteCategory(Guid id)
        {
            _repository.Category.DeleteCategory((HttpContext.Items["entity"] as Category));
            return NoContent();
        }
    }
}