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
    [Route("users")]
    public class UsersController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public UsersController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}", Name = "UserById")]
        [Authorize]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public IActionResult GetUserById(Guid id)
        {
            _logger.LogInfo($"Returned user with id: {id}");
            return Ok(HttpContext.Items["entity"] as User);
        }

        [HttpPut("{id}")]
        [Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdateUser(Guid id, [FromBody] EditedUser editedUser)
        {
            User user = _repository.User.GetUserById(id);

            if (!Crypto.VerifyHashedPassword(user.Password, editedUser.CurrentPassword))
            {
                return BadRequest();
            }

            if (_repository.User.FindByCondition( u => u.Email.Equals(editedUser.Email) && !u.Id.Equals(id)).Any())
                return Conflict("email");

            user.Email = editedUser.Email;

            if (!string.IsNullOrEmpty(editedUser.NewPassword))
            {
                user.Password = Crypto.HashPassword(editedUser.NewPassword);
            }

            _repository.User.UpdateUser(user);
            return NoContent();
        }
    }
}