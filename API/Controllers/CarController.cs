using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarDepo.API.Models;
using CarDepo.Infrastructure.Data;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly CarDepoContext _context;

        public CarController(CarDepoContext context)
        {
            _context = context;
        }

        // GET: api/Car
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Car>>> GetCars()
        {
            return await _context.Cars
                .Include(car => car.Color)
                .Include(car => car.Make)
                .Include(car => car.Owner)                
                .ToListAsync();
        }

        // GET: api/Car/5
        [HttpGet("{CarId}")]
        public async Task<ActionResult<Car>> GetCar(int CarId)
        {
            var car = await _context.Cars
                .Include(car => car.Color)
                .Include(car => car.Make)
                .Include(car => car.Owner)
                .FirstOrDefaultAsync(c=> c.Id == CarId);
            if (car == null)
            {
                return NotFound();
            }
            return car;
        }

        // POST: api/Car
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Car>> PostCar(Car car)
        {
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCar", new { id = car.Id }, car);
        }

        // PUT: api/Car/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{CarId}")]
        public async Task<IActionResult> PutCar(int CarId, Car car)
        {
            if (CarId != car.Id)
            {
                return BadRequest();
            }

            _context.Entry(car).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarExists(CarId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/Car/5
        [HttpDelete("{CarId}")]
        public async Task<IActionResult> DeleteCar(int CarId)
        {
            var car = await _context.Cars.FindAsync(CarId);
            if (car == null)
            {
                return NotFound();
            }

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarExists(int CarId)
        {
            return _context.Cars.Any(e => e.Id == CarId);
        }
    }
}
