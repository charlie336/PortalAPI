using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LIMSWebPortalAPIApp.Data.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using LIMSWebPortalAPIApp.Contracts;

namespace LIMSWebPortalAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _config;
        private readonly ILoggerService _logger;
        public UsersController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            IConfiguration config,
            ILoggerService logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _config = config;
            _logger = logger;
        }
        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AppUserDTO userDTO)
        {
            var location = GetControllerActionNames();
            try
            {
                var username = userDTO.Email;
                var password = userDTO.Password;
                _logger.LogInfo($"{location}: Registration attempt for {username}");
                var user = new IdentityUser { Email = username, UserName = username };
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError($"{location}: {error.Code} {error.Description}");
                    }
                    return InternalError($"{location}: {username} User Registration Attempt Failed");
                }
                return Ok(new { result.Succeeded });
            }
            catch(Exception e)
            {
                return InternalError($"location:{ e.Message} - {e.InnerException}");
            }            
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] AppUserDTO userDTO)
        {
            //var username = userDTO.Username;
            var email = userDTO.Email;
            var password = userDTO.Password;
            var location = GetControllerActionNames();
            var result = await _signInManager.PasswordSignInAsync(email, password, false, false);
            if(result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(email);
                var tokenstring = await GenerateJSONToken(user);
                return Ok(new {token = tokenstring });
            }
            return Unauthorized(userDTO);
        }

        private async Task<string> GenerateJSONToken(IdentityUser user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(roles.Select(s => new Claim(ClaimsIdentity.DefaultNameClaimType, s)));
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                null,
                expires: DateTime.Now.AddDays(14),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller}-{action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }
    }
}
