using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using CarDepo.Application.Services;
// Capa de controlador. La parte de la aplicación sobre la que se haran las peticiones

namespace CarDepo.API.Controllers;
// Clase de Controlador, que implementa la clase base 'ControllerBase'

[Route("/api/[controller]")] // Indica la ruta del controlador
[ApiController] // Para indicar que es un controlador que responde a las llamadas de la API
[AllowAnonymous] // Para indicar que los metodos del controlador pueden usarse sin autenticación 
public class StadisticsController
{
    private readonly StadisticsService _stadisticsService;

    public StadisticsController(StadisticsService stadisticsService)
    {
        _stadisticsService = stadisticsService;
    }

    [HttpGet] // Responde al metodo GET
    [Route("averagekms")]
    public async Task<Double> GetCarKms()
    {
        return await _stadisticsService.GetCarKms();
    }

    [HttpGet] // Responde al metodo GET
    [Route("colorcount")]
    public async Task<IEnumerable> GetColorCount()
    {
        return await _stadisticsService.GetCarColorCount();
    }

    [HttpGet] // Responde al metodo GET
    [Route("makecount")]
    public async Task<IEnumerable> GetMakeCount()
    {
        return await _stadisticsService.GetCarMakeCount();
    }

    [HttpGet] // Responde al metodo GET
    [Route("ownercount")]
    public async Task<IEnumerable> GetOwnerCount()
    {
        return await _stadisticsService.GetCarOwnerCount();
    }

    [HttpGet]
    [Route("cdcarcount")]
    public async Task<IEnumerable> GetAssociatedCarCount()
    {
        return await _stadisticsService.GetCarDriverAssociatedCarCount();
    }

    [HttpGet] // Responde al metodo GET
    [Route("averageprice")]
    public async Task<Decimal> GetAveragePrice()
    {
        return await _stadisticsService.GetAveragePrice();
    }
}
