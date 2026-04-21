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
    public class DriverController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public DriverController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            return await _context.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Driver/5
        [HttpGet("{DriverId}")]
        public async Task<ActionResult<Driver>> GetDriver(int DriverId)
        {
            var driver = await _context.Drivers
                .Include(d => d.Owner)
                .AsNoTracking()
                .FirstOrDefaultAsync(d=> d.Id == DriverId);

            if (driver == null)
            {
                return NotFound();
            }

            return driver;
        }

        // POST: api/Driver
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            _context.Drivers.Add(driver);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        // PUT: api/Driver/5
        [HttpPut("{DriverId}")]
        public async Task<IActionResult> PutDriver(int DriverId, Driver driver)
        {
            if (DriverId != driver.Id)
            {
                return BadRequest();
            }

            _context.Entry(driver).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DriverExists(DriverId))
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

        // DELETE: api/Driver/5
        [HttpDelete("{DriverId}")]
        public async Task<IActionResult> DeleteDriver(int DriverId)
        {
            var driver = await _context.Drivers.FindAsync(DriverId);
            if (driver == null)
            {
                return NotFound();
            }

            _context.Drivers.Remove(driver);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DriverExists(int DriverId)
        {
            return _context.Drivers.Any(e => e.Id == DriverId);
        }
    }
}
