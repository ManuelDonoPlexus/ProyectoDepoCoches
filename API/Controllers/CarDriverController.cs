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
            var cardrivers = await _context.CarConductors
                .Include(cd => cd.Driver)
                .ThenInclude(d => d.Owner)
                .Include(cd => cd.Car)
                .ThenInclude(c => c.Color)
                .AsNoTracking()
                .ToListAsync();
            return cardrivers;
        }

        // GET: api/CarDriver/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarDriver>> GetCarDriver(int id)
        {
            var carDriver = await _context.CarConductors
                .Include(cd => cd.Driver)
                .ThenInclude(d => d.Owner)
                .Include(cd => cd.Car)
                .ThenInclude(c => c.Color)
                .AsNoTracking()
                .FirstOrDefaultAsync(cd => cd.Id == id);

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
            CarDriver newCarDriver = carDriver;
            newCarDriver.Car = carDriver.Car;
            newCarDriver.CarCDId = carDriver.CarCDId;
            newCarDriver.Driver = carDriver.Driver;
            newCarDriver.DriverCDId = carDriver.DriverCDId;

            try
            {
                _context.CarConductors.Add(newCarDriver);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return CreatedAtAction("GetCarDriver", new { id = carDriver.Id }, carDriver);
        }

        // PUT: api/CarDriver/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarDriver(int id, CarDriver carDriver)
        {
            if (id != carDriver.Id)
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
                if (!CarDriverExists(id))
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarDriver(int id)
        {
            var carDriver = await _context.CarConductors.FindAsync(id);
            if (carDriver == null)
            {
                return NotFound();
            }

            _context.CarConductors.Remove(carDriver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarDriverExists(int id)
        {
            return _context.CarConductors.Any(e => e.Id == id);
        }
    }
}
