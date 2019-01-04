using System;
using System.Collections.Generic;
using System.Linq;
using ActionFilters;
using Contracts;
using CryptoHelper;
using Entities.ExtendedModels;
using Entities.Models;
using Entities.ReducedModels;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
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

        [HttpGet, Route("{id}/firebaseToken"), Authorize(Roles = "parent, doctor")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public IActionResult FirebaseLogin(Guid id)
        {
            FirebaseApp firebaseApp = FirebaseApp.DefaultInstance ?? FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(
                    "C:\\Users\\alour\\Documents\\Escritorio\\ULPGC\\Master\\Segundo\\TFM\\prematurekidschat-firebase-adminsdk-gr7so-015971bc6f.json"),
            });

            return Ok(FirebaseAuth.GetAuth(firebaseApp).CreateCustomTokenAsync(id.ToString()).Result);
        }

        [HttpGet("{id}", Name = "UserById"), Authorize]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<User>))]
        public IActionResult GetUserById(Guid id)
        {
            _logger.LogInfo($"Returned user with id: {id}");
            return Ok(HttpContext.Items["entity"] as User);
        }

        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult UpdateUser(Guid id, [FromBody] EditedUser editedUser)
        {
            User user = _repository.User.GetUserById(id);

            if (!Crypto.VerifyHashedPassword(user.Password, editedUser.CurrentPassword))
            {
                return Unauthorized();
            }

            if (_repository.User.FindByCondition(u => u.Email.Equals(editedUser.Email) && !u.Id.Equals(id)).Any())
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