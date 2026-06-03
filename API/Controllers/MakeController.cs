using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Make;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class MakeController : ControllerBase
{
    private readonly MakeService _makeService;

    public MakeController(MakeService makeService)
    {
        _makeService = makeService;
    }

    // GET: api/Make
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Make>>> GetAllMakes()
    {
        var makes = Ok(await _makeService.GetMakes());
        return Ok(makes);
    }

    // GET: api/Make/(id)
    [HttpGet("{MakeId}")] // Responde al metodo GET
    public async Task<ActionResult<Make?>> GetSpecificMake(int MakeId)
    {
        var make = await _makeService.GetMake(MakeId);
        if (make == null) { return NotFound(); }

        return Ok(make);
    }

    // POST: api/Make
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Make?>> CreateMake(Make? make)
    {
        MakeDTO? newmake = await _makeService.InsertMake(make);
        if (newmake != null) { return await GetSpecificMake(newmake.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Make/5
    [HttpPut("{MakeId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyMake(int MakeId, Make newMake)
    {
        MakeDTO? result = await _makeService.UpdateMake(MakeId, newMake);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Make/5
    [HttpDelete("{MakeId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteMake(int MakeId)
    {
        var make = await _makeService.GetMake(MakeId);
        if (make == null) { return NotFound(); }
        await _makeService.DeleteMake(MakeId);
        return NoContent();
    }

}
