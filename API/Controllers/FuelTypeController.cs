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
            return Ok(fuels);
        }

        // GET: api/FuelType/5
        [HttpGet("{FuelId}")]
        public async Task<ActionResult<FuelType?>> GetSpecificFuelType(int FuelId)
        {
            var fuel = Ok(await _fueltypeRepo.GetFuelTypes());
            if (fuel == null) { return NotFound(); }
            return Ok(fuel);
        }

        // POST: api/FuelType
        [HttpPost]
        public async Task<ActionResult<FuelType?>> CreateFuelType(FuelType? fuelType)
        {
            FuelType? newFuel = await _fueltypeRepo.InsertFuelType(fuelType);
            if (newFuel != null) { return await GetSpecificFuelType(newFuel.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/FuelType/5
        [HttpPut("{FuelId}")]
        public async Task<IActionResult> ModifyFuelType(int FuelId, FuelType newFuelType)
        {
            FuelType? result = await _fueltypeRepo.UpdateFuelType(FuelId, newFuelType);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
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
