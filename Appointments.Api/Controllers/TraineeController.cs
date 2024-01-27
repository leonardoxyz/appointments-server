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
        private readonly UnitOfWork _uow;

        public TraineeController(DataContext context, UnitOfWork unitOfWork)
        {
            _context = context;
            _uow = unitOfWork;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Trainee>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetAllAsnync()
        {
            var result = await _context.Trainees.ToListAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddTrainee([FromBody] Trainee request)
        {
            if (request == null)
            {
                return BadRequest();
            }

            await _context.Trainees.AddAsync(request);
            await _uow.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrainee(Guid id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            _context.Trainees.Remove(trainee);
            await _uow.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trainee = await _context.Trainees.FindAsync(id);
            if (trainee == null)
            {
                return NotFound();
            }

            return Ok(trainee);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTrainee(Guid id, [FromBody] Trainee request)
        {
            if (id != request.Id)
            {
                return BadRequest();
            }

            _context.Entry(request).State = EntityState.Modified;
            await _uow.SaveChangesAsync();

            return Ok();
        }
    }
}