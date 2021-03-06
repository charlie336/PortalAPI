﻿using System;
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
using DataLibrary.DTOModels;
using DataLibrary.Data;

namespace LIMSWebPortalAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        //private readonly RoleManager<IdentityUser> _roleManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _config;
        private readonly ILoggerService _logger;
        private readonly IUserData _userData;
        public UsersController(SignInManager<IdentityUser> signInManager, 
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration config,
            ILoggerService logger,
            IUserData userData)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
            _config = config;
            _logger = logger;
            _userData = userData;
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

                //get client id
                var customers = await _userData.GetCustomerbyUserId(user.Id);
                int customerId = -1;
                if (customers != null)
                {
                    if (customers.Count() > 0)
                    {
                        customerId = customers.First().Id;
                    }
                }

                var tokenstring = await GenerateJSONToken(user,customerId);

                var userRoleIds = await _userManager.GetRolesAsync(user);
                List<IdentityUser> roles = new List<IdentityUser>();
                List<string> roleNames = new List<string>();
                foreach (string roleId in userRoleIds)
                {
                    roleNames.Add(roleId);
                    //var role = await _roleManager.FindByNameAsync(roleId);
                    //if (role != null)
                    //{
                    //    var roleName = await _roleManager.GetRoleNameAsync(role);
                    //    roleNames.Add(roleName);
                    //}
                }

                LoginReturnModel loginReturn = new LoginReturnModel()
                {
                    UserId = user.Id,
                    Token = tokenstring,
                    RoleNames = roleNames,
                    CustomerId = customerId,
                };
                return Ok(loginReturn);
                //return Ok(new {token = tokenstring, id = user.Id});
            }
            return Unauthorized(userDTO);
        }

        [HttpPost("Userroles")]
        [Authorize]
        public async Task<IActionResult> GetUserRoles([FromBody] string userId)
        {
            var location = GetControllerActionNames();
            var user = await _userManager.FindByIdAsync(userId);
            var userRoleIds = await _userManager.GetRolesAsync(user);
            List<IdentityUser> roles = new List<IdentityUser>();
            List<string> roleNames = new List<string>();
            foreach (string roleId in userRoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId);
                if (role != null)
                {
                    var roleName = await _roleManager.GetRoleNameAsync(role);
                    roleNames.Add(roleName);
                }
            }
            return Ok(roleNames);
        }

        private async Task<string> GenerateJSONToken(IdentityUser user, int customerId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("CustomerId",customerId.ToString()),
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
