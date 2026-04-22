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
    public class DriverController : ControllerBase
    {
        private readonly IDriverRepository _driverRepo;

        public DriverController(IDriverRepository driverRepo)
        {
            _driverRepo = driverRepo;
        }

        // GET: api/Driver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Driver>>> GetDrivers()
        {
            var drivers = Ok(await _driverRepo.GetDrivers());
            return drivers;
        }

        // GET: api/Driver/5
        [HttpGet("{DriverId}")]
        public async Task<ActionResult<Driver>> GetDriver(int DriverId)
        {
            var driver = Ok(await _driverRepo.GetDriver(DriverId));

            if (driver == null)
            {
                return NotFound();
            }
            return driver;
        }

        // POST: api/Driver
        [HttpPost]
        public async Task<ActionResult<Driver>> PostDriver(Driver driver)
        {
            await _driverRepo.InsertDriver(driver);
            return CreatedAtAction("GetDriver", new { id = driver.Id }, driver);
        }

        // PUT: api/Driver/5
        [HttpPut("{DriverId}")]
        public async Task<IActionResult> PutDriver(int DriverId, Driver driver)
        {
            if (DriverId != driver.Id) { return BadRequest(); }

            try
            {
                await _driverRepo.UpdateDriver(DriverId, driver);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_driverRepo.IfDriverExists(DriverId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // DELETE: api/Driver/5
        [HttpDelete("{DriverId}")]
        public async Task<IActionResult> DeleteDriver(int DriverId)
        {
            var driver = await _driverRepo.GetDriver(DriverId);
            if (driver == null) { return NotFound(); }
            await _driverRepo.GetDriver(DriverId);
            return NoContent();
        }

    }
}
