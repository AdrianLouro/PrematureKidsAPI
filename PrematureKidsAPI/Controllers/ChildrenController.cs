﻿using System;
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
    [Route("children")]
    public class ChildrenController : ControllerBase
    {
        private ILoggerManager _logger;
        private IRepositoryWrapper _repository;

        public ChildrenController(ILoggerManager logger, IRepositoryWrapper repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAllChildren()
        {
            _logger.LogInfo($"Returned all children from database.");
            return Ok(_repository.Child.GetAllChildren());
        }

        [HttpGet("{id}", Name = "ChildById")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Child>))]
        public IActionResult GetChildById(Guid id)
        {
            _logger.LogInfo($"Returned child with id: {id}");
            return Ok(HttpContext.Items["entity"] as Child);
        }

        [HttpGet("{id}/parents")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Child>))]
        public IActionResult GetParents(Guid id)
        {
            _logger.LogInfo($"Returned parents for id: {id}");
            return Ok(_repository.Child.GetParentsOfChild(id));
            //return Ok((HttpContext.Items["entity"] as Child).Parents);
        }

        [HttpGet("{id}/doctors")]
        //[ServiceFilter(typeof(ValidateEntityExistsAttribute<Child>))]
        public IActionResult GetDoctors(Guid id)
        {
            _logger.LogInfo($"Returned doctors for id: {id}");
            return Ok(_repository.Child.GetDoctorsOfChild(id));
            //return Ok((HttpContext.Items["entity"] as Child).Doctors);
        }

        [HttpPost("{id}/parents/{parentId}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult AddParent(Guid id, Guid parentId)
        {
            _repository.ChildParent.CreateChildParent(new ChildParent(id, parentId));
            return NoContent();
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult CreateChild([FromBody] ChildExtended childExtended)
        {
            Child child = new Child(
                childExtended.Id,
                childExtended.Name,
                childExtended.DateOfBirth,
                childExtended.Gender
            );

            _repository.Child.CreateChild(child);
            _repository.ChildParent.CreateChildParent(new ChildParent(child.Id, childExtended.ParentId));

            return CreatedAtRoute("ChildById", new {id = child.Id}, child);
        }

        [HttpPut("{id}"), Authorize(Roles = "parent")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Child>))]
        public IActionResult UpdateChild(Guid id, [FromBody] Child child)
        {
            _repository.Child.UpdateChild(HttpContext.Items["entity"] as Child, child);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateEntityExistsAttribute<Child>))]
        public IActionResult DeleteChild(Guid id)
        {
            _repository.Child.DeleteChild(HttpContext.Items["entity"] as Child);
            return NoContent();
        }
    }
}