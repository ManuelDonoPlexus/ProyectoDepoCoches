using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DriverService _driverService;

        public DriverController(DriverService driverService)
        {
            _driverService = driverService;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetAllDrivers()
        {
            var drivers = Ok(await _driverService.GetDrivers());
            return Ok(drivers);
        }

        // GET: api/Driver/5
        [HttpGet("{DriverId}")]
        public async Task<ActionResult<Driver?>> GetSpecificDriver(int DriverId)
        {
            var driver = Ok(await _driverService.GetDriver(DriverId));

            if (driver == null) { return NotFound(); }
            return Ok(driver);
        }

        // POST: api/Driver
        [HttpPost]
        public async Task<ActionResult<Driver?>> CreateDriver(Driver? driver)
        {
            Driver? newdriver = await _driverService.InsertDriver(driver);
            if (newdriver != null) { return await GetSpecificDriver(newdriver.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Driver/5
        [HttpPut("{DriverId}")]
        public async Task<IActionResult> ModifyDriver(int DriverId, Driver newDriver)
        {
            Driver? result = await _driverService.UpdateDriver(DriverId, newDriver);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
        }

        // DELETE: api/Driver/5
        [HttpDelete("{DriverId}")]
        public async Task<IActionResult> DeleteDriver(int DriverId)
        {
            var driver = await _driverService.GetDriver(DriverId);
            if (driver == null) { return NotFound(); }
            await _driverService.GetDriver(DriverId);
            return NoContent();
        }

    }
}
