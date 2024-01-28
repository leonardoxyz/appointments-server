using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appointments.Domain.Data.Repositories;
using Appointments.Domain.Models;
using Appointments.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace Appointments.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _ipr;
        private readonly IUnitOfWork _uow;
        private readonly DataContext _context;

        public ProjectController(IProjectRepository ipr, IUnitOfWork uow, DataContext context)
        {
            _ipr = ipr;
            _uow = uow;
            _context = context;
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
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProject([FromBody] Project request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            await _ipr.AddAsync(request);
            await _uow.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(Guid id)
        {
            var project = await _ipr.FindByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }

            await _ipr.DeleteAsync(project);
            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}