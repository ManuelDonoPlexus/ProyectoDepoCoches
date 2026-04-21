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
    public class FuelTypeController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public FuelTypeController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/FuelType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetFuelTypes()
        {
            return await _context.FuelTypes.ToListAsync();
        }

        // GET: api/FuelType/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FuelType>> GetFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);

            if (fuelType == null)
            {
                return NotFound();
            }

            return fuelType;
        }

        // POST: api/FuelType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelType>> PostFuelType(FuelType fuelType)
        {
            _context.FuelTypes.Add(fuelType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuelType", new { id = fuelType.Id }, fuelType);
        }

        // PUT: api/FuelType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuelType(int id, FuelType fuelType)
        {
            if (id != fuelType.Id)
            {
                return BadRequest();
            }

            _context.Entry(fuelType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuelTypeExists(id))
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

        // DELETE: api/FuelType/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFuelType(int id)
        {
            var fuelType = await _context.FuelTypes.FindAsync(id);
            if (fuelType == null)
            {
                return NotFound();
            }

            _context.FuelTypes.Remove(fuelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FuelTypeExists(int id)
        {
            return _context.FuelTypes.Any(e => e.Id == id);
        }
    }
}
