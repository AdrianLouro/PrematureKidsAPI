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

        [HttpGet("{id}/exercises")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Category>))]
        public IActionResult GerExercises(Guid id)
        {
            _logger.LogInfo($"Returned exercises for id: {id}");
            return Ok(_repository.Exercise.GetCategoryExercises(id));
        }
    }
}