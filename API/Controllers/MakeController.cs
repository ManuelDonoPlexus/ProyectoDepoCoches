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
    public class MakeController : ControllerBase
    {
        private readonly IMakeRepository _makeRepo;

        public MakeController(IMakeRepository makeRepo)
        {
            _makeRepo = makeRepo;
        }

        // GET: api/Make
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetMakes()
        {
            var makes = Ok(await _makeRepo.GetMakes());
            return makes;
        }

        // GET: api/Make/(id)
        [HttpGet("{MakeId}")]
        public async Task<ActionResult<Make>> GetMake(int MakeId)
        {
            var make = await _makeRepo.GetMake(MakeId);
            if (make == null){return NotFound();}

            return make;
        }

        // POST: api/Make
        [HttpPost]
        public async Task<ActionResult<Make>> PostMake(Make make)
        {
            await _makeRepo.InsertMake(make);
            return CreatedAtAction("GetMake", new { id = make.Id }, make);
        }

        // PUT: api/Make/5
        [HttpPut("{MakeId}")]
        public async Task<IActionResult> PutMake(int MakeId, Make make)
        {
            if (MakeId != make.Id) { return BadRequest(); }

            try
            {
                await _makeRepo.UpdateMake(MakeId, make);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_makeRepo.IfMakeExists(MakeId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // DELETE: api/Make/5
        [HttpDelete("{MakeId}")]
        public async Task<IActionResult> DeleteMake(int MakeId)
        {
            var make = await _makeRepo.GetMake(MakeId);
            if (make == null) { return NotFound(); }
            await _makeRepo.DeleteMake(MakeId);
            return NoContent();
        }

    }
}
