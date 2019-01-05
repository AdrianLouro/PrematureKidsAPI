using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using ActionFilters;
using Contracts;
using CryptoHelper;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using CustomExceptionMiddleware.Models;

namespace PrematureKidsAPI.Controllers
{
    public class AuthController : ControllerBase
    {
        private IRepositoryWrapper _repository;
        private ILoggerManager _logger;

        public AuthController(IRepositoryWrapper repository, ILoggerManager logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpPost, Route("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Login([FromBody] LoginModel user)
        {
            User dbUser = _repository.User.FindByCondition((u => u.Email == user.Email)).FirstOrDefault();

            if (UserDoesNotExist(user, dbUser)) return Unauthorized();
            if (dbUser.Blocked) return Forbid();

            return Ok(new {Token = new JwtSecurityTokenHandler().WriteToken(GetJwtSecurityToken(dbUser))});
        }

        private bool UserDoesNotExist(LoginModel user, User dbUser)
        {
            return dbUser == null || (dbUser != null && !Crypto.VerifyHashedPassword(dbUser.Password, user.Password));
        }

        private JwtSecurityToken GetJwtSecurityToken(User dbUser)
        {
            return new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>()
                {
                    new Claim("role", dbUser.Role),
                    new Claim("sub", dbUser.Id.ToString()),
                },
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                    SecurityAlgorithms.HmacSha256
                )
            );
        }
    }
}