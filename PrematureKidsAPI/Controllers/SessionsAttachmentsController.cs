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
    [Route("sessionsAttachments")]
    public class SessionsAttachmentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public SessionsAttachmentsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}", Name = "SessionAttachmentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<SessionAttachment>))]
        public IActionResult GetSessionAttachmentById(Guid id)
        {
            _logger.LogInfo($"Returned session attachment with id: {id}");
            return Ok(_repository.SessionAttachment.GetSessionAttachmentById(id));
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateSessionAttachment([FromBody] SessionAttachment sessionAttachment)
        {
            _repository.SessionAttachment.CreateSessionAttachment(sessionAttachment);
            return CreatedAtRoute("SessionAttachmentById", new {id = sessionAttachment.Id}, new SessionAttachmentExtended(sessionAttachment));
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<SessionAttachment>))]
        public IActionResult DeleteSessionAttachment(Guid id)
        {
            _repository.SessionAttachment.DeleteSessionAttachment(
                (HttpContext.Items["entity"] as SessionAttachment));
            return NoContent();
        }
    }
}