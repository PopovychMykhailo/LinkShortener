using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace LinkShortener.Resource.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CheckAccountController : ControllerBase
    {
        private Guid UserId => Guid.Parse(User.Claims.Single(c => c.Type == ClaimTypes.NameIdentifier).Value);
        private string Email => User.Claims.Single(c => c.Type == ClaimTypes.Email).Value;
        private string Role => User.Claims.Single(c => c.Type == ClaimTypes.Role).Value;



        [HttpGet(nameof(GetEmail))]
        [Authorize]
        public IActionResult GetEmail()
        {
            return Ok($"Your email: {Email}");
        }


        [Authorize]
        [HttpGet(nameof(GetRole))]
        public IActionResult GetRole()
        {
            return Ok($"Your role: {Role}");
        }


        [Authorize(Roles = "Admin")]
        [HttpGet(nameof(IsAdmin))]
        public IActionResult IsAdmin()
        {
            return Ok(true);
        }
    }
}
