using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Appointments.Domain.Models;
using Appointments.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Appointments.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TraineeController : ControllerBase
    {
        private readonly DataContext _context;

        public TraineeController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAllAsnync()
        {
            var result = await _context.Trainees.ToListAsync();
            return Ok(result);
        }
    }
}