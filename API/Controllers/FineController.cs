using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FineController : ControllerBase
    {
        private readonly FineService _fineService;

        public FineController(FineService fineService)
        {
            _fineService = fineService;
        }

        // GET: api/Fine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetAllFines()
        {
            var fines = Ok(await _fineService.GetFines());
            return Ok(fines);
        }

        // GET: api/Fine/5
        [HttpGet("{FineId}")]
        public async Task<ActionResult<Fine?>> GetSpecificFine(int FineId)
        {
            var fine = Ok(await _fineService.GetFine(FineId));
            return Ok(fine);
        }

        // POST: api/Fine
        [HttpPost]
        public async Task<ActionResult<Fine?>> CreateFine(Fine? fine)
        {
            FineDTO? newFine = await _fineService.InsertFine(fine);
            if (newFine != null) { return await GetSpecificFine(newFine.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Fine/5
        [HttpPut("{FineId}")]
        public async Task<IActionResult> ModifyFine(int FineId, Fine newFine)
        {
            FineDTO? result = await _fineService.UpdateFine(FineId, newFine);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/Fine/5
        [HttpDelete("{FineId}")]
        public async Task<IActionResult> DeleteFine(int FineId)
        {
            var fine = await _fineService.GetFine(FineId);
            if (fine == null) { return NotFound(); }
            await _fineService.DeleteFine(FineId);
            return NoContent();
        }
    }
}
