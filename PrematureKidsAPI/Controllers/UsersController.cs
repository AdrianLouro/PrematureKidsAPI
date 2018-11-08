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