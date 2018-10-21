using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
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

        public AuthController(IRepositoryWrapper repository)
        {
            _repository = repository;
        }

        [HttpPost, Route("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public IActionResult Login([FromBody] LoginModel user)
        {
            User dbUser = _repository.User.FindByCondition((u => u.Email == user.Email)).FirstOrDefault();

            if (userDoesNotExist(user, dbUser)) return Unauthorized();

            return Ok(new {Token = new JwtSecurityTokenHandler().WriteToken(getJwtSecurityToken(dbUser))});
        }

        private bool userDoesNotExist(LoginModel user, User dbUser)
        {
            return dbUser == null || (dbUser != null && !Crypto.VerifyHashedPassword(dbUser.Password, user.Password));
        }

        private JwtSecurityToken getJwtSecurityToken(User dbUser)
        {
            return new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>() {new Claim("role", dbUser.Role)},
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                    SecurityAlgorithms.HmacSha256
                )
            );
        }
    }
}