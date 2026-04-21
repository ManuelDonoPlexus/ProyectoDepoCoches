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
    public class FineController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public FineController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Fine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetFines()
        {            
            return await _context.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .ToListAsync();
        }

        // GET: api/Fine/5
        [HttpGet("{FineId}")]
        public async Task<ActionResult<Fine>> GetFine(int FineId)
        {
            var fine = await _context.Fines
                .Include(f => f.Owner)
                .Include(f => f.Car)
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == FineId);

            if (fine == null)
            {
                return NotFound();
            }

            return fine;
        }

        // POST: api/Fine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fine>> PostFine(Fine fine)
        {
            _context.Fines.Add(fine);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFine", new { id = fine.Id }, fine);
        }

        // PUT: api/Fine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{FineId}")]
        public async Task<IActionResult> PutFine(int FineId, Fine fine)
        {
            if (FineId != fine.Id)
            {
                return BadRequest();
            }

            _context.Entry(fine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FineExists(FineId))
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

        // DELETE: api/Fine/5
        [HttpDelete("{FineId}")]
        public async Task<IActionResult> DeleteFine(int FineId)
        {
            var fine = await _context.Fines.FindAsync(FineId);
            if (fine == null)
            {
                return NotFound();
            }

            _context.Fines.Remove(fine);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FineExists(int FineId)
        {
            return _context.Fines.Any(e => e.Id == FineId);
        }
    }
}
