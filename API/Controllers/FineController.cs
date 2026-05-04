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
    public class FineController : ControllerBase
    {
        private readonly IFineRepository _fineRepository;

        public FineController(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        // GET: api/Fine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetAllFines()
        {
            var fines = Ok(await _fineRepository.GetFines());
            return Ok(fines);
        }

        // GET: api/Fine/5
        [HttpGet("{FineId}")]
        public async Task<ActionResult<Fine?>> GetSpecificFine(int FineId)
        {
            var fine = Ok(await _fineRepository.GetFine(FineId));
            return Ok(fine);
        }

        // POST: api/Fine
        [HttpPost]
        public async Task<ActionResult<Fine?>> CreateFine(Fine? fine)
        {
            Fine? newFine = await _fineRepository.InsertFine(fine);
            if (newFine != null) { return await GetSpecificFine(newFine.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Fine/5
        [HttpPut("{FineId}")]
        public async Task<IActionResult> ModifyFine(int FineId, Fine newFine)
        {
            Fine? result = await _fineRepository.UpdateFine(FineId, newFine);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
        }

        // DELETE: api/Fine/5
        [HttpDelete("{FineId}")]
        public async Task<IActionResult> DeleteFine(int FineId)
        {
            var fine = await _fineRepository.GetFine(FineId);
            if (fine == null) { return NotFound(); }

            await _fineRepository.DeleteFine(FineId);
            return NoContent();
        }
    }
}
