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
    [Route("parents")]
    public class ParentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ParentsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllParents()
        {
            _logger.LogInfo($"Returned all parents from database.");
            return Ok(_repository.Parent.GetAllParents());
        }

        [HttpGet("{id}", Name = "ParentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetParentById(Guid id)
        {
            _logger.LogInfo($"Returned parentUser with id: {id}");
            return Ok(HttpContext.Items["entity"] as Parent);
        }

        [HttpGet("{id}/user")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            _logger.LogInfo($"Returned user without details for id: {id}");
            return Ok(_repository.User.GetUserWithoutDetail((HttpContext.Items["entity"] as Parent).UserId));
        }


        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateParent([FromBody] ParentUser parentUser)
        {
            var userId = _repository.User.CreateUser(new User(
                parentUser.UserId,
                parentUser.Email,
                Crypto.HashPassword(parentUser.Password),
                parentUser.Role
            ));

            var parent = new Parent(
                userId,
                parentUser.Name,
                parentUser.IdNumber,
                parentUser.Telephone,
                userId
            );

            _repository.Parent.CreateParent(parent);
            return CreatedAtRoute("ParentById", new {id = parentUser.UserId}, parent);
        }


        [HttpPut("{id}"), Authorize(Roles = "parent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult UpdateParent(Guid id, [FromBody] Parent parent)
        {
            _repository.Parent.UpdateParent(HttpContext.Items["entity"] as Parent, parent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult DeleteParent(Guid id)
        {
            _repository.User.DeleteUser(
                _repository.User
                    .FindByCondition((user => user.Id == (HttpContext.Items["entity"] as Parent).UserId))
                    .FirstOrDefault()
            );
            return NoContent();
        }
    }
}