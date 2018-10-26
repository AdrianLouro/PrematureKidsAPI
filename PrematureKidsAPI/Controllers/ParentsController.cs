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
        public IActionResult GetAllParents(string idNumber)
        {
            _logger.LogInfo($"Returned all parents from database.");
            return Ok(
                idNumber == null
                    ? _repository.Parent.GetAllParents()
                    : _repository.Parent.FindByCondition(p =>
                        String.Equals(p.IdNumber, idNumber, StringComparison.CurrentCultureIgnoreCase)
                    )
            );
        }

        [HttpGet("{id}", Name = "ParentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetParentById(Guid id)
        {
            _logger.LogInfo($"Returned parent with id: {id}");
            return Ok(HttpContext.Items["entity"] as Parent);
        }

        [HttpGet("{id}/children")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetChildren(Guid id)
        {
            _logger.LogInfo($"Returned children for id: {id}");
            return Ok(_repository.Parent.GetChildrenOfParent(id));
            //return Ok((HttpContext.Items["entity"] as Parent).Children);
        }

        [HttpGet("{id}/doctors")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetDoctors(Guid id)
        {
            _logger.LogInfo($"Returned doctors for id: {id}");
            return Ok(_repository.Parent.GetDoctorsOfParent(id));
        }

        [HttpGet("{id}/user")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Parent>))]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            _logger.LogInfo($"Returned user without details for id: {id}");
            return Ok(_repository.User.GetUserWithoutDetail((HttpContext.Items["entity"] as Parent).Id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateParent([FromBody] ParentUser parentUser)
        {
            Guid userId = _repository.User.CreateUser(new User(
                parentUser.Id,
                parentUser.Email,
                Crypto.HashPassword(parentUser.Password),
                parentUser.Role
            ));

            Parent parent = new Parent(
                userId,
                parentUser.Name,
                parentUser.IdNumber,
                parentUser.Telephone
            );

            _repository.Parent.CreateParent(parent);
            return CreatedAtRoute("ParentById", new {id = parentUser.Id}, parent);
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
                    .FindByCondition((user => user.Id == (HttpContext.Items["entity"] as Parent).Id))
                    .FirstOrDefault()
            );
            return NoContent();
        }
    }
}