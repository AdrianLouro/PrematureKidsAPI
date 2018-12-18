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
    [Route("administrators")]
    public class AdministratorsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public AdministratorsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}", Name = "AdministratorById"), Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Administrator>))]
        public IActionResult GetAdministratorById(Guid id)
        {
            _logger.LogInfo($"Returned administrator with id: {id}");
            return Ok(HttpContext.Items["entity"] as Administrator);
        }

        [HttpGet("{id}/user"), Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Administrator>))]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            _logger.LogInfo($"Returned user without details for id: {id}");
            return Ok(_repository.User.GetUserWithoutDetail((HttpContext.Items["entity"] as Administrator).Id));
        }

        [HttpPut("{id}"), Authorize(Roles = "administrator")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Administrator>))]
        public IActionResult UpdateAdministrator(Guid id, [FromBody] Administrator administrator)
        {
            _repository.Administrator.UpdateAdministrator(HttpContext.Items["entity"] as Administrator, administrator);
            return NoContent();
        }

    }
}