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
    public class CarDriverController : ControllerBase
    {
        private readonly ICarDriverRepository _cardriverRepo;

        public CarDriverController(ICarDriverRepository cardriverRepo)
        {
            _cardriverRepo = cardriverRepo;
        }

        // GET: api/CarDriver
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarDriver?>>> GetAllCarDrivers()
        {
            var cardrivers = Ok(await _cardriverRepo.GetCarDrivers());
            return cardrivers;
        }

        // GET: api/CarDriver/5
        [HttpGet("{CarDriverId}")]
        public async Task<ActionResult<CarDriver?>> GetSpecificCarDriver(int CarDriverId)
        {
            var carDriver = await _cardriverRepo.GetCarDriver(CarDriverId);
            if (carDriver == null) { return NotFound(); }
            return carDriver;
        }

        // POST: api/CarDriver
        [HttpPost]
        public async Task<ActionResult<CarDriver?>?> CreateCarDriver(CarDriver? carDriver)
        {
            CarDriver? newcardriver = await _cardriverRepo.InsertCarDriver(carDriver);
            if (newcardriver != null) { return await GetSpecificCarDriver(newcardriver.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/CarDriver/5
        [HttpPut("{CarDriverId}")]
        public async Task<IActionResult> ModifyCarDriver(int CarDriverId, CarDriver newCarDriver)
        {
            CarDriver? result = await _cardriverRepo.UpdateCarDriver(CarDriverId, newCarDriver);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
        }

        // DELETE: api/CarDriver/5
        [HttpDelete("{CarDriverId}")]
        public async Task<IActionResult> DeleteCarDriver(int CarDriverId)
        {
            var cardriver = await _cardriverRepo.GetCarDriver(CarDriverId);
            if (cardriver == null) { return NotFound(); }

            await _cardriverRepo.DeleteCarDriver(CarDriverId);

            return NoContent();
        }
    }
}
