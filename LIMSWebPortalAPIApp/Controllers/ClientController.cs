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
    [Authorize]
    public class ClientController : ControllerBase
    {

        private ILoggerService _logger;
        public ClientController(ILoggerService logger)
        {
            _logger = logger;
        }

        [Authorize(Roles = "CUSTOMER_ADMIN")]
        [Authorize(Roles = "SUPER_ADMIN")]
        //public async Task<IActionResult> GetContacts([FromBody] string userId)
        //{
        //    //get clientId from userId
        //    //get contacts from clientId
        //}


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
