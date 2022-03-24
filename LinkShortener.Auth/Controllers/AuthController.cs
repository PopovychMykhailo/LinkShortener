using LinkShortener.Auth.Common;
using LinkShortener.Auth.Entities;
using LinkShortener.Auth.Models;
using LinkShortener.Resource.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

namespace LinkShortener.Auth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private IOptions<AuthOptions> AuthOptions { get; }
        private AuthDbContext Context { get; }


        public AuthController(IOptions<AuthOptions> authOptions, AuthDbContext context)
        {
            AuthOptions = authOptions;
            Context = context;
        }


        [Route(nameof(SignIn))]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInModel request)
        {
            var user = AuthenticateUser(request.Email, request.Password);

            // Error Sign in
            if (user == null)
                return Unauthorized();

            // Generate JWT-token
            var token = GenerateJWT(user);
            return Ok(new { acces_token = token });
        }


        [Route(nameof(SignUp))]
        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpModel requst)
        {
            // Check email for uniqueness
            if (Context.Accounts.FirstOrDefault(a => a.Email == requst.Email) != null)
                return BadRequest(new { ErrorText = "Email already exists. Please enter other email." });

            var user = Context.Accounts.Add(new AccountEntity 
            { 
                Id = Guid.Empty,
                Email = requst.Email,
                Password = requst.Password,
                Role = Role.User
            });
            Context.SaveChanges();

            // Error Sign up
            if (user == null)
                return Unauthorized();

            return Ok();
        }


        private AccountModel AuthenticateUser(string email, string password)
        {
            return (AccountModel)Context.Accounts.FirstOrDefault(x => x.Email == email && x.Password == password);
        }

        private string GenerateJWT(AccountModel user)
        {
            var authParams = AuthOptions.Value;
            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            #region Generate claims
            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim("role", user.Role.ToString())
            };
            #endregion

            var jwtToken = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}
