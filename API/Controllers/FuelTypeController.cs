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
    public class FuelTypeController : ControllerBase
    {
        private readonly IFuelTypeRepository _fueltypeRepo;

        public FuelTypeController(IFuelTypeRepository fueltypeRepo)
        {
            _fueltypeRepo = fueltypeRepo;
        }

        // GET: api/FuelType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetAllFuelTypes()
        {
            var fuels = Ok(await _fueltypeRepo.GetFuelTypes());
            return fuels;
        }

        // GET: api/FuelType/5
        [HttpGet("{FuelId}")]
        public async Task<ActionResult<FuelType>> GetSpecificFuelType(int FuelId)
        {
            var fuel = Ok(await _fueltypeRepo.GetFuelTypes());
            if (fuel == null) { return NotFound(); }
            return fuel;
        }

        // POST: api/FuelType
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FuelType>> CreateFuelType(FuelType fuelType)
        {
            await _fueltypeRepo.InsertFuelType(fuelType);
            return CreatedAtAction("GetFuelType", new { id = fuelType.Id }, fuelType);
        }

        // PUT: api/FuelType/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{FuelId}")]
        public async Task<IActionResult> ModifyFuelType(int FuelId, FuelType fuelType)
        {
            if (FuelId != fuelType.Id) { return BadRequest(); }

            try { await _fueltypeRepo.UpdateFuelType(FuelId, fuelType); }
            catch (DbUpdateConcurrencyException)
            {
                if (!_fueltypeRepo.IfFuelTypeExists(FuelId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // DELETE: api/FuelType/5
        [HttpDelete("{FuelId}")]
        public async Task<IActionResult> DeleteFuelType(int FuelId)
        {
            var fuel = _fueltypeRepo.GetFuelType(FuelId);
            if (fuel == null) { return NotFound(); }
            await _fueltypeRepo.DeleteFuelType(FuelId);
            return NoContent();
        }
    }
}
