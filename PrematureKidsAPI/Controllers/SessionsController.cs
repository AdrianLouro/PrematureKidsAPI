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
    [Route("sessions")]
    public class SessionsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public SessionsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllSessions(Guid categoryId)
        {
            _logger.LogInfo($"Returned all sessions from database.");
            return Ok(_repository.Session.GetAllSessions());
        }

        [HttpGet("{id}/videos")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Session>))]
        public IActionResult GetVideos(Guid id)
        {
            _logger.LogInfo($"Returned videos for id: {id}");
            return Ok(_repository.SessionAttachment.GetSessionVideos((HttpContext.Items["entity"] as Session).Id));
        }

        [HttpGet("{id}/images")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Session>))]
        public IActionResult GetImages(Guid id)
        {
            _logger.LogInfo($"Returned images for id: {id}");
            return Ok(_repository.SessionAttachment.GetSessionImages((HttpContext.Items["entity"] as Session).Id));
        }

        [HttpGet("{id}", Name = "SessionById")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Session>))]
        public IActionResult GetSessionById(Guid id)
        {
            _logger.LogInfo($"Returned session with id: {id}");
            return Ok(_repository.Session.GetSessionById(id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateSession([FromBody] Session session)
        {
            _repository.Session.CreateSession(session);
            return CreatedAtRoute("SessionById", new {id = session.Id}, session);
        }

        [HttpPut("{id}"), Authorize(Roles = "doctor, parent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Session>))]
        public IActionResult UpdateSession(Guid id, [FromBody] Session session)
        {
            _repository.Session.UpdateSession(HttpContext.Items["entity"] as Session, session);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Session>))]
        public IActionResult DeleteSession(Guid id)
        {
            _repository.Session.DeleteSession((HttpContext.Items["entity"] as Session));
            return NoContent();
        }
    }
}