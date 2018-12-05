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
    [Route("exercisesAttachments")]
    public class ExercisesAttachmentsController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ExercisesAttachmentsController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet("{id}", Name = "ExerciseAttachmentById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ExerciseAttachment>))]
        public IActionResult GetExerciseAttachmentById(Guid id)
        {
            _logger.LogInfo($"Returned exercise attachment with id: {id}");
            return Ok(_repository.ExerciseAttachment.GetExerciseAttachmentById(id));
        }

        [HttpPost, DisableRequestSizeLimit]
        public IActionResult CreateExerciseAttachment(IFormFile file)
        {
            Guid guid = Guid.NewGuid();

            string uploadsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "exercises");

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

            ExerciseAttachment exerciseAttachment = new ExerciseAttachment(
                guid,
                Request.Form["name"] + (Request.Form["type"].Equals("video") ? ".mp4" : ".png"),
                Request.Form["type"],
                new Guid(Request.Form["exerciseId"])
            );

            _repository.ExerciseAttachment.CreateExerciseAttachment(exerciseAttachment);
            return CreatedAtRoute(
                "ExerciseAttachmentById",
                new {id = exerciseAttachment.Id},
                new ExerciseAttachmentExtended(exerciseAttachment)
            );
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<ExerciseAttachment>))]
        public IActionResult DeleteExerciseAttachment(Guid id)
        {
            System.IO.File.Delete(
                Path.Combine(
                    Directory.GetCurrentDirectory(), "wwwroot", "uploads", "exercises",
                    id + ((HttpContext.Items["entity"] as ExerciseAttachment).Type.Equals("video") ? ".mp4" : ".png")
                )
            );

            _repository.ExerciseAttachment.DeleteExerciseAttachment(
                (HttpContext.Items["entity"] as ExerciseAttachment));
            return NoContent();
        }
    }
}