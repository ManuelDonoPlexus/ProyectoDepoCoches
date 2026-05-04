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
    public class CarController : ControllerBase
    {
        private readonly ICarRepository _carRepo;

        public CarController(ICarRepository carRepo)
        {
            _carRepo = carRepo;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            var cars = Ok(await _carRepo.GetCars());
            return Ok(cars);
        }

        // GET: api/Car/5
        [HttpGet("{CarId}")]
        public async Task<ActionResult<Car?>> GetSpecificCar(int CarId)
        {
            var car = Ok(await _carRepo.GetCar(CarId));
            if (car == null) { return NotFound(); }
            return Ok(car);
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car?>?> CreateCar(Car? car)
        {
            Car? newcar = await _carRepo.InsertCar(car);
            if (newcar != null) { return await GetSpecificCar(newcar.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Car/5
        [HttpPut("{CarId}")]
        public async Task<IActionResult> ModifyCar(int CarId, Car newCar)
        {
            Car? result = await _carRepo.UpdateCar(CarId, newCar);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
        }

        // DELETE: api/Car/5
        [HttpDelete("{CarId}")]
        public async Task<IActionResult> DeleteCar(int CarId)
        {
            await _carRepo.DeleteCar(CarId);
            return NoContent();
        }
    }
}
