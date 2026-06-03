using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Car;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'


[Route("api/[controller]")] // Indica la ruta del controlador
[ApiController] // Para indicar que es un controlador que responde a las llamadas de la API
[Authorize] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class CarController : ControllerBase
{
    private readonly CarService _carService;

    public CarController(CarService carService)
    {
        _carService = carService;
    }

    // GET: api/Car
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Car>>> GetAllCars()
    {
        var cars = Ok(await _carService.GetCars());
        return Ok(cars);
    }

    // GET: api/Car/5
    [HttpGet("{CarId}")] // Responde al metodo GET
    public async Task<ActionResult<Car?>> GetSpecificCar(int CarId)
    {
        var car = Ok(await _carService.GetCar(CarId));
        if (car == null) { return NotFound(); }
        return Ok(car);
    }

    // POST: api/Car
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Car?>?> CreateCar(Car? car)
    {
        CarDTO? newcar = await _carService.InsertCar(car);
        if (newcar != null) { return await GetSpecificCar(newcar.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Car/5
    [HttpPut("{CarId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyCar(int CarId, Car newCar)
    {
        CarDTO? result = await _carService.UpdateCar(CarId, newCar);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Car/5
    [HttpDelete("{CarId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteCar(int CarId)
    {
        var car = await _carService.GetCar(CarId);
        if (car == null) { return NotFound(); }
        await _carService.DeleteCar(CarId);
        return NoContent();
    }
}

