using Microsoft.AspNetCore.Mvc;
using CarDepo.API.Models;
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;
using CarDepo.API.DTOs.Owner;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("api/[controller]")] // Indica la ruta del controlador
[Authorize] // Para indicar que es un controlador que responde a las llamadas de la API
[ApiController] // Para indicar que el controlador requiere de autenticación para usar sus metodos
public class OwnerController : ControllerBase
{
    private readonly OwnerService _ownerService;

    public OwnerController(OwnerService ownerService)
    {
        _ownerService = ownerService;
    }

    // GET: api/Owner
    [HttpGet] // Responde al metodo GET
    public async Task<ActionResult<IEnumerable<Owner>>> GetAllOwners()
    {
        var owners = Ok(await _ownerService.GetOwners());
        return Ok(owners);
    }

    // GET: api/Owner/5
    [HttpGet("{OwnerId}")] // Responde al metodo GET
    public async Task<ActionResult<Owner?>> GetSpecificOwner(int OwnerId)
    {
        var owner = Ok(await _ownerService.GetOwner(OwnerId));
        if (owner == null) { return NotFound(); }
        return Ok(owner);
    }

    // POST: api/Owner
    [HttpPost] // Responde al metodo POST
    public async Task<ActionResult<Owner?>> CreateOwner(Owner? owner)
    {
        OwnerDTO? newowner = await _ownerService.InsertOwner(owner);
        if (newowner != null) { return await GetSpecificOwner(newowner.Id); }
        else { return BadRequest(); }
    }

    // PUT: api/Owner/5
    [HttpPut("{OwnerId}")] // Responde al metodo PUT
    public async Task<IActionResult> ModifyOwner(int OwnerId, Owner newOwner)
    {
        OwnerDTO? result = await _ownerService.UpdateOwner(OwnerId, newOwner);
        if (result != null) { return Ok(result); }
        else { return BadRequest(); }
    }

    // DELETE: api/Owner/5
    [HttpDelete("{OwnerId}")] // Responde al metodo DELETE
    public async Task<IActionResult> DeleteOwner(int OwnerId)
    {
        var owner = await _ownerService.GetOwner(OwnerId);
        if (owner == null) { return NotFound(); }
        await _ownerService.DeleteOwner(OwnerId);
        return NoContent();
    }
}