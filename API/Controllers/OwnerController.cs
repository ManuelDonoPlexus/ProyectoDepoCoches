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
    public class OwnerController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public OwnerController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Owner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            return await _context.Owners.ToListAsync();
        }

        // GET: api/Owner/5
        [HttpGet("{OwnerId}")]
        public async Task<ActionResult<Owner>> GetOwner(int OwnerId)
        {
            var owner = await _context.Owners.FindAsync(OwnerId);

            if (owner == null)
            {
                return NotFound();
            }

            return owner;
        }

        // POST: api/Owner
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Owner>> PostOwner(Owner owner)
        {
            _context.Owners.Add(owner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        }

        // PUT: api/Owner/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{OwnerId}")]
        public async Task<IActionResult> PutOwner(int OwnerId, Owner owner)
        {
            if (OwnerId != owner.Id)
            {
                return BadRequest();
            }

            _context.Entry(owner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OwnerExists(OwnerId))
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

        // DELETE: api/Owner/5
        [HttpDelete("{OwnerId}")]
        public async Task<IActionResult> DeleteOwner(int OwnerId)
        {
            var owner = await _context.Owners.FindAsync(OwnerId);
            if (owner == null)
            {
                return NotFound();
            }

            _context.Owners.Remove(owner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OwnerExists(int OwnerId)
        {
            return _context.Owners.Any(e => e.Id == OwnerId);
        }
    }
}
