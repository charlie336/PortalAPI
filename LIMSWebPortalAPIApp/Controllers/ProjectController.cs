using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using DataLibrary.Data;
using LIMSWebPortalAPIApp.Contracts;
using LIMSWebPortalAPIApp.Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LIMSWebPortalAPIApp.Controllers
{
    /// <summary>
    /// Interact with projects in database
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectData _projectData;
        private readonly ILoggerService _logger;
        private readonly IMapper _mapper;
        public ProjectController(IProjectData projectData, ILoggerService logger, IMapper mapper)
        {
            _projectData = projectData;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProjects(int customerId)
        {
            try
            {
                _logger.LogInfo($"Gat projects from the customer {customerId}");
                var projects = await _projectData.GetAll(customerId);
                var respones = _mapper.Map<IList<ProjectDTO>>(projects);
                _logger.LogInfo("Successful!");
                return Ok(respones);
            }
            catch(Exception e)
            {
                _logger.LogError($"{e.Message} -{e.InnerException}");
                return StatusCode(500, "Something wrong. PLease contact the administrator");
            }
        }

        public IActionResult Authenticate()
        {
            var userClaims = new List<Claim>() { new Claim(ClaimTypes.Name)}
        }
    }
}
