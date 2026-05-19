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

        public StadisticsController(AuthUtilities utilities, ICarRepository carrepo, IMakeRepository makerepo, IDriverRepository driverepo)
        {
            _authutils = utilities;
            _carrepo = carrepo;
            _makerepo = makerepo;
            _driverrepo = driverepo;
        }

        [HttpGet]
        public async Task<IEnumerable<Car>> GetCars()
        {
            IEnumerable<Car> cars = await _carrepo.GetCars();
            return cars;
        }

        [HttpGet]
        public async Task<IEnumerable<Make>> GetMake()
        {
            IEnumerable<Make> makes = await _makerepo.GetMakes();
            return makes;
        }

        [HttpGet]
        public async Task<IEnumerable<Driver>> GetDrivers()
        {
            IEnumerable<Driver> drivers = await _driverrepo.GetDrivers();
            return drivers;
        }
    }
}