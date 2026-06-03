using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Driver;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class DriverController : ControllerBase
{
    private readonly DriverService _driverService;

    public DriverController(DriverService driverService)
    {
        _driverService = driverService;
    }

    // GET: api/Driver
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Driver>>> GetAllDrivers()
    {
        var drivers = Ok(await _driverService.GetDrivers());
        return Ok(drivers);
    }

    // GET: api/Driver/5
    [HttpGet("{DriverId}")] // Responde al metodo GET
    public async Task<ActionResult<Driver?>> GetSpecificDriver(int DriverId)
    {
        var driver = Ok(await _driverService.GetDriver(DriverId));

        if (driver == null) { return NotFound(); }
        return Ok(driver);
    }

    // POST: api/Driver
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Driver?>> CreateDriver(Driver? driver)
    {
        DriverDTO? newdriver = await _driverService.InsertDriver(driver);
        if (newdriver != null) { return await GetSpecificDriver(newdriver.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Driver/5
    [HttpPut("{DriverId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyDriver(int DriverId, Driver newDriver)
    {
        DriverDTO? result = await _driverService.UpdateDriver(DriverId, newDriver);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Driver/5
    [HttpDelete("{DriverId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteDriver(int DriverId)
    {
        var driver = await _driverService.GetDriver(DriverId);
        if (driver == null) { return NotFound(); }
        await _driverService.GetDriver(DriverId);
        return NoContent();
    }

}