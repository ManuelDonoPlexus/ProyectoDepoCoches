using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly IOwnerRepository _ownerRepo;

        public OwnerController(IOwnerRepository ownerRepo)
        {
            _ownerRepo = ownerRepo;
        }

        // GET: api/Owner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetAllOwners()
        {
            var owenrs = Ok(await _ownerRepo.GetOwners());
            return owenrs;
        }

        // GET: api/Owner/5
        [HttpGet("{OwnerId}")]
        public async Task<ActionResult<Owner>> GetSpecificOwner(int OwnerId)
        {
            var owner = Ok(await _ownerRepo.GetOwner(OwnerId));
            if (owner == null) { return NotFound(); }
            return owner;
        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<Owner>> CreateOwner(Owner owner)
        {
            await _ownerRepo.InsertOwner(owner);
            return CreatedAtAction("GetOwner", new { id = owner.Id }, owner);
        }

        // PUT: api/Owner/5
        [HttpPut("{OwnerId}")]
        public async Task<IActionResult> ModifyOwner(int OwnerId, Owner owner)
        {
            if (OwnerId != owner.Id) { return BadRequest(); }

            try { await _ownerRepo.UpdateOwner(OwnerId, owner); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_ownerRepo.IfOwnerExists(OwnerId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // DELETE: api/Owner/5
        [HttpDelete("{OwnerId}")]
        public async Task<IActionResult> DeleteOwner(int OwnerId)
        {
            var owner = await _ownerRepo.GetOwner(OwnerId);
            if (owner == null) { return NotFound(); }
            await _ownerRepo.DeleteOwner(OwnerId);
            return NoContent();
        }
    }
}
