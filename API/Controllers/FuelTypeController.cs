using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.FuelType;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class FuelTypeController : ControllerBase
{
    private readonly FuelTypeService _fueltypeService;

    public FuelTypeController(FuelTypeService fueltypeService)
    {
        _fueltypeService = fueltypeService;
    }

    // GET: api/FuelType
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<FuelType>>> GetAllFuelTypes()
    {
        var fuels = Ok(await _fueltypeService.GetFuelTypes());
        return Ok(fuels);
    }

    // GET: api/FuelType/5
    [HttpGet("{FuelId}")] // Responde al metodo GET
    public async Task<ActionResult<FuelType?>> GetSpecificFuelType(int FuelId)
    {
        var fuel = Ok(await _fueltypeService.GetFuelTypes());
        if (fuel == null) { return NotFound(); }
        return Ok(fuel);
    }

    // POST: api/FuelType
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<FuelType?>> CreateFuelType(FuelType? fuelType)
    {
        FuelTypeDTO? newFuel = await _fueltypeService.InsertFuelType(fuelType);
        if (newFuel != null) { return await GetSpecificFuelType(newFuel.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/FuelType/5
    [HttpPut("{FuelId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyFuelType(int FuelId, FuelType newFuelType)
    {
        FuelTypeDTO? result = await _fueltypeService.UpdateFuelType(FuelId, newFuelType);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/FuelType/5
    [HttpDelete("{FuelId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteFuelType(int FuelId)
    {
        var fuel = _fueltypeService.GetFuelType(FuelId);
        if (fuel == null) { return NotFound(); }
        await _fueltypeService.DeleteFuelType(FuelId);
        return NoContent();
    }
}