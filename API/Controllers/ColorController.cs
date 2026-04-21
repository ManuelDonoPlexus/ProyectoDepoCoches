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
    public class ColorController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public ColorController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Color
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetColors()
        {
            return await _context.Colors.ToListAsync();
        }

        // GET: api/Color/5
        [HttpGet("{ColorId}")]
        public async Task<ActionResult<Color>> GetColor(int ColorId)
        {
            var color = await _context.Colors.FindAsync(ColorId);

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        // POST: api/Color
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Color>> PostColor(Color color)
        {
            _context.Colors.Add(color);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetColor", new { id = color.Id }, color);
        }

        // PUT: api/Color/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{ColorId}")]
        public async Task<IActionResult> PutColor(int ColorId, Color color)
        {
            if (ColorId != color.Id)
            {
                return BadRequest();
            }

            _context.Entry(color).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorExists(ColorId))
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

        // DELETE: api/Color/5
        [HttpDelete("{ColorId}")]
        public async Task<IActionResult> DeleteColor(int ColorId)
        {
            var color = await _context.Colors.FindAsync(ColorId);
            if (color == null)
            {
                return NotFound();
            }

            _context.Colors.Remove(color);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ColorExists(int ColorId)
        {
            return _context.Colors.Any(e => e.Id == ColorId);
        }
    }
}
