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
            return Ok(colors);
        }

        // GET: api/Color/5
        [HttpGet("{ColorId}")]
        public async Task<ActionResult<Color?>> GetSpecificColor(int ColorId)
        {
            var color = Ok(await _colorRepo.GetColor(ColorId));
            if (color == null) { return NotFound(); }
            return Ok(color);
        }

        // POST: api/Color
        [HttpPost]
        public async Task<ActionResult<Color?>> CreateColor(Color? color)
        {
            Color? newcolor = await _colorRepo.InsertColor(color);
            if (newcolor != null){ return await GetSpecificColor(newcolor.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Color/5
        [HttpPut("{ColorId}")]
        public async Task<IActionResult> ModifyColor(int ColorId, Color newColor)
        {
            Color? result = await _colorRepo.UpdateColor(ColorId, newColor);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
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
