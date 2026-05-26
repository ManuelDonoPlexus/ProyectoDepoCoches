using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StadisticsController
    {
        private readonly StadisticsService _stadisticsService;

        public StadisticsController(StadisticsService stadisticsService)
        {
            _stadisticsService = stadisticsService;
        }

        [HttpGet]
        [Route("averagekms")]
        public async Task<Double> GetCarKms()
        {
            return await _stadisticsService.GetCarKms();
        }

        [HttpGet]
        [Route("colorcount")]
        public async Task<IEnumerable> GetColorCount()
        {
            return await _stadisticsService.GetCarColorCount();
        }

        [HttpGet]
        [Route("makecount")]
        public async Task<IEnumerable> GetMakeCount()
        {
            return await _stadisticsService.GetCarMakeCount();
        }

        [HttpGet]
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

        [HttpGet]
        [Route("averageprice")]
        public async Task<Decimal> GetAveragePrice()
        {
            return await _stadisticsService.GetAveragePrice();
        }
    }
}