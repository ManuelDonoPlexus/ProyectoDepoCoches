using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarDriverController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public CarDriverController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/CarDriver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDriver>>> GetCarConductors()
        {
            return await _context.CarConductors
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/CarDriver/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDriver>> GetCarDriver(int CDId)
        {
            var carDriver = await _context.CarConductors
                .Include(cd => cd.Driver)
                .Include(cd => cd.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == CDId);

            if (carDriver == null)
            {
                return NotFound();
            }

            return carDriver;
        }

        // POST: api/CarDriver
        [HttpPost]
        public async Task<ActionResult<CarDriver>> PostCarDriver(CarDriver carDriver)
        {
            _context.CarConductors.Add(carDriver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCarDriver", new { id = carDriver.Id }, carDriver);
        }

        // PUT: api/CarDriver/5
        [HttpPut("{CDId}")]
        public async Task<IActionResult> PutCarDriver(int CDId, CarDriver carDriver)
        {
            if (CDId != carDriver.Id)
            {
                return BadRequest();
            }

            _context.Entry(carDriver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarDriverExists(CDId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/CarDriver/5
        [HttpDelete("{CDId}")]
        public async Task<IActionResult> DeleteCarDriver(int CDId)
        {
            var carDriver = await _context.CarConductors.FindAsync(CDId);
            if (carDriver == null)
            {
                return NotFound();
            }

            _context.CarConductors.Remove(carDriver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarDriverExists(int CDId)
        {
            return _context.CarConductors.Any(e => e.Id == CDId);
        }
    }
}
