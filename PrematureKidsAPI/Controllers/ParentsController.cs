using System;
using Contracts;
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

        [HttpGet("{id}")]
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
                    _logger.LogInfo($"Returned parent with id: {id}");
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
    }
}