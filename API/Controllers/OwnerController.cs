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
using Microsoft.AspNetCore.Authorization;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
            var owners = Ok(await _ownerRepo.GetOwners());
            return Ok(owners);
        }

        // GET: api/Owner/5
        [HttpGet("{OwnerId}")]
        public async Task<ActionResult<Owner?>> GetSpecificOwner(int OwnerId)
        {
            var owner = Ok(await _ownerRepo.GetOwner(OwnerId));
            if (owner == null) { return NotFound(); }
            return Ok(owner);
        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<Owner?>> CreateOwner(Owner? owner)
        {
            Owner? newowner = await _ownerRepo.InsertOwner(owner);
            if (newowner != null) { return await GetSpecificOwner(newowner.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Owner/5
        [HttpPut("{OwnerId}")]
        public async Task<IActionResult> ModifyOwner(int OwnerId, Owner newOwner)
        {
            Owner? result = await _ownerRepo.UpdateOwner(OwnerId, newOwner);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
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
