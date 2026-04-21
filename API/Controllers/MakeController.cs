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
    public class MakeController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public MakeController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Make
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            var makes = await _context.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .ToListAsync();
            return makes;
        }

        // GET: api/Make/(id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Make>> GetMake(int id)
        {
            var make = await _context.Makes
                .Include(m => m.FuelType)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (make == null)
            {
                return NotFound();
            }

            return make;
        }

        // POST: api/Make
        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
            Make newMake = make;
            newMake.FuelTypeId = make.FuelTypeId;
            newMake.FuelType = make.FuelType;

            try
            {
                _context.Makes.Add(newMake);
                await _context.SaveChangesAsync();
            }
            catch (System.Exception)
            {
                throw;
            }

            return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // PUT: api/Make/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMake(int id, Make make)
        {
            if (id != make.Id)
            {
                return BadRequest();
            }

            _context.Entry(make).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MakeExists(id))
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

        // DELETE: api/Make/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMake(int id)
        {
            var make = await _context.Makes.FindAsync(id);
            if (make == null)
            {
                return NotFound();
            }

            _context.Makes.Remove(make);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MakeExists(int id)
        {
            return _context.Makes.Any(e => e.Id == id);
        }
    }
}
