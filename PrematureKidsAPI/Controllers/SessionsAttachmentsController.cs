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

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult CreateSessionAttachment(IFormFile file)
        {
            Guid guid = Guid.NewGuid();

            string uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "sessions");

            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            using (var stream = new FileStream(
                //Path.Combine(uploadsDirectory, guid + Path.GetExtension(file.FileName)),
                Path.Combine(uploadsDirectory, guid + (Request.Form["type"].Equals("video") ? ".mp4" : ".png")),
                FileMode.Create)
            )
            {
                file.CopyTo(stream);
            }

            SessionAttachment sessionAttachment = new SessionAttachment(
                guid,
                Request.Form["name"] + (Request.Form["type"].Equals("video") ? ".mp4" : ".png"),
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
            System.IO.File.Delete(
                Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot", "uploads", "sessions",
                    id + ((HttpContext.Items["entity"] as SessionAttachment).Type.Equals("video") ? ".mp4" : ".png")
                )
            );

            _repository.SessionAttachment.DeleteSessionAttachment(
                (HttpContext.Items["entity"] as SessionAttachment));
            return NoContent();
        }
    }
}