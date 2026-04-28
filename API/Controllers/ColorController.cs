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

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly IColorRepository _colorRepo;

        public ColorController(IColorRepository colorRepo)
        {
            _colorRepo = colorRepo;
        }

        // GET: api/Color
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetAllColors()
        {
            var colors = Ok(await _colorRepo.GetColors());
            return colors;
        }

        // GET: api/Color/5
        [HttpGet("{ColorId}")]
        public async Task<ActionResult<Color?>> GetSpecificColor(int ColorId)
        {
            var color = Ok(await _colorRepo.GetColor(ColorId));

            if (color == null)
            {
                return NotFound();
            }

            return color;
        }

        // POST: api/Color
        [HttpPost]
        public async Task<ActionResult<Color?>> CreateColor(Color? color)
        {
            Color? newcolor = await _colorRepo.InsertColor(color);
            if (newcolor != null)
            {
                ActionResult<Color?> result = await GetSpecificColor(newcolor.Id);
                if (result != null) { return result; }
                else { return BadRequest(); }
            }
            else { return BadRequest(); }
        }

        // PUT: api/Color/5
        [HttpPut("{ColorId}")]
        public async Task<IActionResult> ModifyColor(int ColorId, Color color)
        {
            if (ColorId != color.Id) { return BadRequest(); }

            try
            {
                await _colorRepo.UpdateColor(ColorId, color);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_colorRepo.IfColorExists(ColorId)) { return NotFound(); }
                else { throw; }
            }

            return NoContent();
        }

        // DELETE: api/Color/5
        [HttpDelete("{ColorId}")]
        public async Task<IActionResult> DeleteColor(int ColorId)
        {
            var color = await _colorRepo.GetColor(ColorId);
            if (color == null) { return NotFound(); }

            await _colorRepo.DeleteColor(ColorId);
            return NoContent();
        }
    }
}
