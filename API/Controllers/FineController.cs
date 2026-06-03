using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Fine;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class FineController : ControllerBase
{
    private readonly FineService _fineService;

    public FineController(FineService fineService)
    {
        _fineService = fineService;
    }

    // GET: api/Fine
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Fine>>> GetAllFines()
    {
        var fines = Ok(await _fineService.GetFines());
        return Ok(fines);
    }

    // GET: api/Fine/5
    [HttpGet("{FineId}")] // Responde al metodo GET
    public async Task<ActionResult<Fine?>> GetSpecificFine(int FineId)
    {
        var fine = Ok(await _fineService.GetFine(FineId));
        return Ok(fine);
    }

    // POST: api/Fine
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Fine?>> CreateFine(Fine? fine)
    {
        FineDTO? newFine = await _fineService.InsertFine(fine);
        if (newFine != null) { return await GetSpecificFine(newFine.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Fine/5
    [HttpPut("{FineId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyFine(int FineId, Fine newFine)
    {
        FineDTO? result = await _fineService.UpdateFine(FineId, newFine);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Fine/5
    [HttpDelete("{FineId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteFine(int FineId)
    {
        var fine = await _fineService.GetFine(FineId);
        if (fine == null) { return NotFound(); }
        await _fineService.DeleteFine(FineId);
        return NoContent();
    }
}