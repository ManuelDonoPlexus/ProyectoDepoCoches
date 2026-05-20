using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;
using CarDepo.Infrastructure.Repositories;
using CarDepo.API.DTOs;
using CarDepo.API.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Collections;

namespace CarDepo.API.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class StadisticsController
    {
        public readonly AuthUtilities _authutils;
        public readonly ICarRepository _carrepo;
        public readonly IMakeRepository _makerepo;
        public readonly IDriverRepository _driverrepo;
        public readonly ICarDriverRepository _cardriverrepo;

        public StadisticsController(AuthUtilities utilities, ICarRepository carrepo, IMakeRepository makerepo, IDriverRepository driverepo, ICarDriverRepository cardriverrepo)
        {
            _authutils = utilities;
            _carrepo = carrepo;
            _makerepo = makerepo;
            _driverrepo = driverepo;
            _cardriverrepo = cardriverrepo;
        }

        // Estadisticas de coches

        [HttpGet]
        [Route("averagekms")]
        public async Task<Double> GetCarKms()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.Average(c => c.Kms);
        }

        [HttpGet]
        [Route("carcolorcount")]
        public async Task<IEnumerable> GetCarColorCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.ColorId);
        }

        [HttpGet]
        [Route("carmakecount")]
        public async Task<IEnumerable> GetCarMakeCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.MakeId);
        }

        [HttpGet]
        [Route("carownercount")]
        public async Task<IEnumerable> GetCarOwnerCount()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars.CountBy(c => c.OwnerId);
        }

        // Estadisticas de conductores

        [HttpGet]
        [Route("cdcarcount")]
        public async Task<IEnumerable> GetCarDriverAssociatedCarCount()
        {
            IEnumerable<CarDriver> drivers = await _cardriverrepo.GetCarDrivers();
            return drivers.CountBy(d => d.CarCDId);
        }

        // Estadisticas de marcas

        [HttpGet]
        [Route("averageprice")]
        public async Task<Decimal> GetAveragePrice()
        {
            IEnumerable<Make> makes = await _makerepo.GetMakes();
            return makes.Average(m => m.Price);
        }
    }
}