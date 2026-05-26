using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        public OwnerController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        // GET: api/Owner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Owner>>> GetAllOwners()
        {
            var owners = Ok(await _ownerService.GetOwners());
            return Ok(owners);
        }

        // GET: api/Owner/5
        [HttpGet("{OwnerId}")]
        public async Task<ActionResult<Owner?>> GetSpecificOwner(int OwnerId)
        {
            var owner = Ok(await _ownerService.GetOwner(OwnerId));
            if (owner == null) { return NotFound(); }
            return Ok(owner);
        }

        // POST: api/Owner
        [HttpPost]
        public async Task<ActionResult<Owner?>> CreateOwner(Owner? owner)
        {
            Owner? newowner = await _ownerService.InsertOwner(owner);
            if (newowner != null) { return await GetSpecificOwner(newowner.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Owner/5
        [HttpPut("{OwnerId}")]
        public async Task<IActionResult> ModifyOwner(int OwnerId, Owner newOwner)
        {
            Owner? result = await _ownerService.UpdateOwner(OwnerId, newOwner);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/Owner/5
        [HttpDelete("{OwnerId}")]
        public async Task<IActionResult> DeleteOwner(int OwnerId)
        {
            var owner = await _ownerService.GetOwner(OwnerId);
            if (owner == null) { return NotFound(); }
            await _ownerService.DeleteOwner(OwnerId);
            return NoContent();
        }
    }
}
