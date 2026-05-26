using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class FuelTypeController : ControllerBase
    {
        private readonly FuelTypeService _fueltypeService;

        public FuelTypeController(FuelTypeService fueltypeService)
        {
            _fueltypeService = fueltypeService;
        }

        // GET: api/FuelType
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FuelType>>> GetAllFuelTypes()
        {
            var fuels = Ok(await _fueltypeService.GetFuelTypes());
            return Ok(fuels);
        }

        // GET: api/FuelType/5
        [HttpGet("{FuelId}")]
        public async Task<ActionResult<FuelType?>> GetSpecificFuelType(int FuelId)
        {
            var fuel = Ok(await _fueltypeService.GetFuelTypes());
            if (fuel == null) { return NotFound(); }
            return Ok(fuel);
        }

        // POST: api/FuelType
        [HttpPost]
        public async Task<ActionResult<FuelType?>> CreateFuelType(FuelType? fuelType)
        {
            FuelType? newFuel = await _fueltypeService.InsertFuelType(fuelType);
            if (newFuel != null) { return await GetSpecificFuelType(newFuel.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/FuelType/5
        [HttpPut("{FuelId}")]
        public async Task<IActionResult> ModifyFuelType(int FuelId, FuelType newFuelType)
        {
            FuelType? result = await _fueltypeService.UpdateFuelType(FuelId, newFuelType);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/FuelType/5
        [HttpDelete("{FuelId}")]
        public async Task<IActionResult> DeleteFuelType(int FuelId)
        {
            var fuel = _fueltypeService.GetFuelType(FuelId);
            if (fuel == null) { return NotFound(); }
            await _fueltypeService.DeleteFuelType(FuelId);
            return NoContent();
        }
    }
}
