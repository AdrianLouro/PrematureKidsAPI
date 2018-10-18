using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PrematureKidsAPI.Models;

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
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user == null) return BadRequest("Invalid client request");

            User dbUser = _repository.User.FindByCondition(
                (u => u.Email == user.Email && u.Password == user.Password)
            ).FirstOrDefault();

            if (dbUser == null) return Unauthorized();

            var tokenOptions = new JwtSecurityToken(
                issuer: "http://localhost:5000",
                audience: "http://localhost:5000",
                claims: new List<Claim>() {new Claim("role", dbUser.Role)},
                expires: DateTime.Now.AddMonths(3),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345")),
                    SecurityAlgorithms.HmacSha256
                )
            );

            return Ok(new {Token = new JwtSecurityTokenHandler().WriteToken(tokenOptions)});
        }
    }
}