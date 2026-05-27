using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MakeController : ControllerBase
    {
        private readonly MakeService _makeService;

        public MakeController(MakeService makeService)
        {
            _makeService = makeService;
        }

        // GET: api/Make
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Make>>> GetAllMakes()
        {
            var makes = Ok(await _makeService.GetMakes());
            return Ok(makes);
        }

        // GET: api/Make/(id)
        [HttpGet("{MakeId}")]
        public async Task<ActionResult<Make?>> GetSpecificMake(int MakeId)
        {
            var make = await _makeService.GetMake(MakeId);
            if (make == null){return NotFound();}

            return Ok(make);
        }

        // POST: api/Make
        [HttpPost]
        public async Task<ActionResult<Make?>> CreateMake(Make? make)
        {
            MakeInsertDTO? newmake = await _makeService.InsertMake(make);
            if (newmake != null){return await GetSpecificMake(newmake.Id);}
            else { return BadRequest(); }
        }

        // PUT: api/Make/5
        [HttpPut("{MakeId}")]
        public async Task<IActionResult> ModifyMake(int MakeId, Make newMake)
        {
            MakeUpdateDTO? result = await _makeService.UpdateMake(MakeId, newMake);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/Make/5
        [HttpDelete("{MakeId}")]
        public async Task<IActionResult> DeleteMake(int MakeId)
        {
            var make = await _makeService.GetMake(MakeId);
            if (make == null) { return NotFound(); }
            await _makeService.DeleteMake(MakeId);
            return NoContent();
        }

    }
}
