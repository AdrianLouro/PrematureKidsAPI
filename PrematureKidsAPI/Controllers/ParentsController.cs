using System;
using System.Linq;
using Contracts;
using Entities.ExtendedModels;
using Entities.Models;
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
        //[HttpGet, Authorize(Roles = "parent")]
        public IActionResult GetAllParents()
        {
            try
            {
                var parents = _repository.Parent.GetAllParents();

                _logger.LogInfo($"Returned all parents from database.");

                return Ok(parents);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetAllParents action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}", Name = "ParentById")]
        public IActionResult GetParentById(Guid id)
        {
            try
            {
                var parent = _repository.Parent.GetParentById(id);

                if (parent == null)
                {
                    _logger.LogError($"Parent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned parentUser with id: {id}");
                    return Ok(parent);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetParentById action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}/user")]
        public IActionResult GetUserWithoutDetails(Guid id)
        {
            try
            {
                var user = _repository.User.GetUserWithoutDetail(id);

                if (user.Id.Equals(Guid.Empty))
                {
                    _logger.LogError($"User with id: {id}, hasn't been found in db.");
                    return NotFound();
                }
                else
                {
                    _logger.LogInfo($"Returned user without details for id: {id}");
                    return Ok(user);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside GetUserWithoutDetails action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult CreateParent([FromBody] ParentUser parentUser)
        {
            try
            {
                if (parentUser == null)
                {
                    _logger.LogError("Parent object sent from client is null.");
                    return BadRequest("Parent object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid parentUser object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var userId = _repository.User.CreateUser(new User(
                    parentUser.UserId,
                    parentUser.Email,
                    parentUser.Password,
                    parentUser.Role
                ));

                var parent = new Parent(
                    parentUser.ParentId,
                    parentUser.Name,
                    parentUser.IdNumber,
                    parentUser.Telephone,
                    userId
                );

                _repository.Parent.CreateParent(parent);

                return CreatedAtRoute("ParentById", new {id = parentUser.UserId}, parent);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside CreateParent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateParent(Guid id, [FromBody] Parent parent)
        {
            try
            {
                if (parent == null)
                {
                    _logger.LogError("Parent object sent from client is null.");
                    return BadRequest("Parent object is null");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError("Invalid parent object sent from client.");
                    return BadRequest("Invalid model object");
                }

                var dbParent = _repository.Parent.GetParentById(id);
                if (dbParent == null)
                {
                    _logger.LogError($"Parent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.Parent.UpdateParent(dbParent, parent);

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside UpdateParent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteParent(Guid id)
        {
            try
            {
                var parent = _repository.Parent.GetParentById(id);
                if (parent == null)
                {
                    _logger.LogError($"Parent with id: {id}, hasn't been found in db.");
                    return NotFound();
                }

                _repository.User.DeleteUser(_repository.User.FindByCondition(user => user.Id == parent.UserId)
                    .FirstOrDefault());

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong inside DeleteParent action: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}