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
        public StadisticUtilities _stadisticUtilities;

        public StadisticsController(StadisticUtilities stadisticUtilities)
        {
            _stadisticUtilities = stadisticUtilities;
        }

        [HttpGet]
        [Route("averagekms")]
        public async Task<Decimal> GetCarKms()
        {
            return await _stadisticUtilities.GetAveragePrice();
        }

        [HttpGet]
        [Route("colorcount")]
        public async Task<IEnumerable> GetColorCount()
        {
            return await _stadisticUtilities.GetCarColorCount();
        }

        [HttpGet]
        [Route("makecount")]
        public async Task<IEnumerable> GetMakeCount()
        {
            return await _stadisticUtilities.GetCarMakeCount();
        }

        [HttpGet]
        [Route("ownercount")]
        public async Task<IEnumerable> GetOwnerCount()
        {
            return await _stadisticUtilities.GetCarOwnerCount();
        }

        [HttpGet]
        [Route("cdcarcount")]
        public async Task<IEnumerable> GetAssociatedCarCount()
        {
            return await _stadisticUtilities.GetCarDriverAssociatedCarCount();
        }

        [HttpGet]
        [Route("averageprice")]
        public async Task<Decimal> GetAveragePrice()
        {
            return await _stadisticUtilities.GetAveragePrice();
        }
    }
}