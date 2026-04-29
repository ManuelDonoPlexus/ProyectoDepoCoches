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
        public async Task<ActionResult<IEnumerable<Make>>> GetAllMakes()
        {
            var makes = Ok(await _makeRepo.GetMakes());
            return makes;
        }

        // GET: api/Make/(id)
        [HttpGet("{MakeId}")]
        public async Task<ActionResult<Make?>> GetSpecificMake(int MakeId)
        {
            var make = await _makeRepo.GetMake(MakeId);
            if (make == null){return NotFound();}

            return make;
        }

        // POST: api/Make
        [HttpPost]
        public async Task<ActionResult<Make?>> CreateMake(Make? make)
        {
            Make? newmake = await _makeRepo.InsertMake(make);
            if (newmake != null){return await GetSpecificMake(newmake.Id);}
            else { return BadRequest(); }
        }

        // PUT: api/Make/5
        [HttpPut("{MakeId}")]
        public async Task<IActionResult> ModifyMake(int MakeId, Make newMake)
        {
            Make? result = await _makeRepo.UpdateMake(MakeId, newMake);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
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
