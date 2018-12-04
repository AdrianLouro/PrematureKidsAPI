using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ActionFilters;
using Contracts;
using CryptoHelper;
using Entities.ExtendedModels;
using Entities.Models;
using Entities.ReducedModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PrematureKidsAPI.Controllers
{
    [Route("sessionsAttachments")]
    public class SessionsAttachmentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;
        private IHostingEnvironment _environment;

        public SessionsAttachmentsController(ILoggerManager logger, IRepositoryWrapper repository,
            IHostingEnvironment environment)
        {
            _logger = logger;
            _repository = repository;
            _environment = environment;
        }

        [HttpGet("{id}", Name = "SessionAttachmentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<SessionAttachment>))]
        public IActionResult GetSessionAttachmentById(Guid id)
        {
            _logger.LogInfo($"Returned session attachment with id: {id}");
            return Ok(_repository.SessionAttachment.GetSessionAttachmentById(id));
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult CreateSessionAttachment(IFormFile file)
        {
            Guid guid = Guid.NewGuid();

            string uploadsDirectory =
                Path.Combine(Path.Combine(_environment.WebRootPath, "uploads"), "sessions");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            using (var stream = new FileStream(
                Path.Combine(uploadsDirectory, guid + Path.GetExtension(file.FileName)),
                FileMode.Create)
            )
            {
                file.CopyTo(stream);
            }

            SessionAttachment sessionAttachment = new SessionAttachment(
                guid,
                Request.Form["name"],
                Request.Form["type"],
                new Guid(Request.Form["sessionId"])
            );

            _repository.SessionAttachment.CreateSessionAttachment(sessionAttachment);
            return CreatedAtRoute(
                "SessionAttachmentById",
                new {id = sessionAttachment.Id},
                new SessionAttachmentExtended(sessionAttachment)
            );
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