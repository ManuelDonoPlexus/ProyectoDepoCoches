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
    public class FineController : ControllerBase
    {
        private readonly IFineRepository _fineRepository;

        public FineController(IFineRepository fineRepository)
        {
            _fineRepository = fineRepository;
        }

        // GET: api/Fine
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fine>>> GetFines()
        {
            var fines = Ok( _fineRepository.GetFines());
            return fines;
        }

        // GET: api/Fine/5
        [HttpGet("{FineId}")]
        public async Task<ActionResult<Fine>> GetFine(int FineId)
        {
            var fine = Ok( _fineRepository.GetFine(FineId));
            return fine;
        }

        // POST: api/Fine
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fine>> PostFine(Fine fine)
        {
            await _fineRepository.InsertFine(fine);
            return CreatedAtAction("GetFine", new { id = fine.Id }, fine);
        }

        // PUT: api/Fine/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{FineId}")]
        public async Task<IActionResult> PutFine(int FineId, Fine fine)
        {
            if (FineId != fine.Id) { return BadRequest(); }

            try
            {
                await _fineRepository.UpdateFine(FineId, fine);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_fineRepository.IfFineExists(FineId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
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
