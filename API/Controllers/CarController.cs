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
            return cars;
        }

        // GET: api/Car/5
        [HttpGet("{CarId}")]
        public async Task<ActionResult<Car?>> GetSpecificCar(int CarId)
        {
            var car = Ok(await _carRepo.GetCar(CarId));
            if (car == null) { return NotFound(); }
            return car;
        }

        // POST: api/Car
        // Recibe una entidad "Car" nulable como parametro.
        [HttpPost]
        public async Task<ActionResult<Car?>?> CreateCar(Car? car)
        {
            // Ejecuta el metodo InsertCar del repositorio con el parametro de la funcionalidad
            Car? newcar = await _carRepo.InsertCar(car);

            // Si el resultado no es nulo, de devuelve la entidad creada des pues de buscarla por su id. 
            // Si es nulo, se devuelve BadRequest para indicar un problema con la adición 
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
