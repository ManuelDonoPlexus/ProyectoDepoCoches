using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Car;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarService _carService;

        public CarController(CarService carService)
        {
            _carService = carService;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
        {
            var cars = Ok(await _carService.GetCars());
            return Ok(cars);
        }

        // GET: api/Car/5
        [HttpGet("{CarId}")]
        public async Task<ActionResult<Car?>> GetSpecificCar(int CarId)
        {
            var car = Ok(await _carService.GetCar(CarId));
            if (car == null) { return NotFound(); }
            return Ok(car);
        }

        // POST: api/Car
        [HttpPost]
        public async Task<ActionResult<Car?>?> CreateCar(Car? car)
        {
            CarDTO? newcar = await _carService.InsertCar(car);
            if (newcar != null) { return await GetSpecificCar(newcar.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Car/5
        [HttpPut("{CarId}")]
        public async Task<IActionResult> ModifyCar(int CarId, Car newCar)
        {
            CarDTO? result = await _carService.UpdateCar(CarId, newCar);
            if(result != null) { return Ok(result); }
            else { return BadRequest(); }
        }

        // DELETE: api/Car/5
        [HttpDelete("{CarId}")]
        public async Task<IActionResult> DeleteCar(int CarId)
        {
            var car = await _carService.GetCar(CarId);
            if (car == null) { return NotFound(); }
            await _carService.DeleteCar(CarId);
            return NoContent();
        }
    }
}
