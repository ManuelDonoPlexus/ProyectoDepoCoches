using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Color;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class ColorController : ControllerBase
{
    private readonly ColorService _colorSevice;

    public ColorController(ColorService colorService)
    {
        _colorSevice = colorService;
    }

    // GET: api/Color
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Color>>> GetAllColors()
    {
        var colors = Ok(await _colorSevice.GetColors());
        return Ok(colors);
    }

    // GET: api/Color/5
    [HttpGet("{ColorId}")] // Responde al metodo GET
    public async Task<ActionResult<Color?>> GetSpecificColor(int ColorId)
    {
        var color = Ok(await _colorSevice.GetColor(ColorId));
        if (color == null) { return NotFound(); }
        return Ok(color);
    }

    // POST: api/Color
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Color?>> CreateColor(Color? color)
    {
        ColorDTO? newcolor = await _colorSevice.InsertColor(color);
        if (newcolor != null) { return await GetSpecificColor(newcolor.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Color/5
    [HttpPut("{ColorId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyColor(int ColorId, Color newColor)
    {
        ColorDTO? result = await _colorSevice.UpdateColor(ColorId, newColor);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Color/5
    [HttpDelete("{ColorId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteColor(int ColorId)
    {
        var color = await _colorSevice.GetColor(ColorId);
        if (color == null) { return NotFound(); }
        await _colorSevice.DeleteColor(ColorId);
        return NoContent();
    }
}
