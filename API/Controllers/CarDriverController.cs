using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarDriverController : ControllerBase
    {
        private readonly CarDriverService _cardriverService;

        public CarDriverController(CarDriverService cardriverService)
        {
            _cardriverService = cardriverService;
        }

        // GET: api/CarDriver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDriver?>>> GetAllCarDrivers()
        {
            var cardrivers = Ok(await _cardriverService.GetCarDrivers());
            return Ok(cardrivers);
        }

        // GET: api/CarDriver/5
        [HttpGet("{CarDriverId}")]
        public async Task<ActionResult<CarDriver?>> GetSpecificCarDriver(int CarDriverId)
        {
            var carDriver = Ok(await _cardriverService.GetCarDriver(CarDriverId));
            if (carDriver == null) { return NotFound(); }
            return Ok(carDriver);
        }

        // POST: api/CarDriver
        [HttpPost]
        public async Task<ActionResult<CarDriver?>?> CreateCarDriver(CarDriver? carDriver)
        {
            CarDriver? newcardriver = await _cardriverService.InsertCarDriver(carDriver);
            if (newcardriver != null) { return await GetSpecificCarDriver(newcardriver.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/CarDriver/5
        [HttpPut("{CarDriverId}")]
        public async Task<IActionResult> ModifyCarDriver(int CarDriverId, CarDriver newCarDriver)
        {
            CarDriver? result = await _cardriverService.UpdateCarDriver(CarDriverId, newCarDriver);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/CarDriver/5
        [HttpDelete("{CarDriverId}")]
        public async Task<IActionResult> DeleteCarDriver(int CarDriverId)
        {
            var cardriver = await _cardriverService.GetCarDriver(CarDriverId);
            if (cardriver == null) { return NotFound(); }
            await _cardriverService.DeleteCarDriver(CarDriverId);
            return NoContent();
        }
    }
}
