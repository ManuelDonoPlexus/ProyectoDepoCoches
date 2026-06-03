using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.CarDriver;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class CarDriverController : ControllerBase
{
    private readonly CarDriverService _cardriverService;

    public CarDriverController(CarDriverService cardriverService)
    {
        _cardriverService = cardriverService;
    }

    // GET: api/CarDriver
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<CarDriver?>>> GetAllCarDrivers()
    {
        var cardrivers = Ok(await _cardriverService.GetCarDrivers());
        return Ok(cardrivers);
    }

    // GET: api/CarDriver/5
    [HttpGet("{CarDriverId}")] // Responde al metodo GET
    public async Task<ActionResult<CarDriver?>> GetSpecificCarDriver(int CarDriverId)
    {
        var carDriver = Ok(await _cardriverService.GetCarDriver(CarDriverId));
        if (carDriver == null) { return NotFound(); }
        return Ok(carDriver);
    }

    // POST: api/CarDriver
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<CarDriver?>?> CreateCarDriver(CarDriver? carDriver)
    {
        CarDriverDTO? newcardriver = await _cardriverService.InsertCarDriver(carDriver);
        if (newcardriver != null) { return await GetSpecificCarDriver(newcardriver.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/CarDriver/5
    [HttpPut("{CarDriverId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyCarDriver(int CarDriverId, CarDriver newCarDriver)
    {
        CarDriverDTO? result = await _cardriverService.UpdateCarDriver(CarDriverId, newCarDriver);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/CarDriver/5
    [HttpDelete("{CarDriverId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteCarDriver(int CarDriverId)
    {
        var cardriver = await _cardriverService.GetCarDriver(CarDriverId);
        if (cardriver == null) { return NotFound(); }
        await _cardriverService.DeleteCarDriver(CarDriverId);
        return NoContent();
    }
}