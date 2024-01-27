using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appointments.Domain.Data.Repositories;
using Appointments.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _ipr;
        private readonly IUnitOfWork _uow;

        public ProjectController(IProjectRepository ipr, IUnitOfWork uow)
        {
            _ipr = ipr;
            _uow = uow;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAllAsync()
        {
            try
            {
                var result = await _ipr.GetAllAsync();
                return Ok(result);
            }
            catch (System.Exception)
            {
                return BadRequest("Error");
            }
        }
    }
}