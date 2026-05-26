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
using Microsoft.AspNetCore.Authorization;
using CarDepo.Application.Services;

namespace CarDepo.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ColorController : ControllerBase
    {
        private readonly ColorService _colorSevice;

        public ColorController(ColorService colorService)
        {
            _colorSevice = colorService;
        }

        // GET: api/Color
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Color>>> GetAllColors()
        {
            var colors = Ok(await _colorSevice.GetColors());
            return Ok(colors);
        }

        // GET: api/Color/5
        [HttpGet("{ColorId}")]
        public async Task<ActionResult<Color?>> GetSpecificColor(int ColorId)
        {
            var color = Ok(await _colorSevice.GetColor(ColorId));
            if (color == null) { return NotFound(); }
            return Ok(color);
        }

        // POST: api/Color
        [HttpPost]
        public async Task<ActionResult<Color?>> CreateColor(Color? color)
        {
            Color? newcolor = await _colorSevice.InsertColor(color);
            if (newcolor != null){ return await GetSpecificColor(newcolor.Id); }
            else { return BadRequest(); }
        }

        // PUT: api/Color/5
        [HttpPut("{ColorId}")]
        public async Task<IActionResult> ModifyColor(int ColorId, Color newColor)
        {
            Color? result = await _colorSevice.UpdateColor(ColorId, newColor);
            if(result != null) { return NoContent(); }
            else { return BadRequest(); }
        }

        // DELETE: api/Color/5
        [HttpDelete("{ColorId}")]
        public async Task<IActionResult> DeleteColor(int ColorId)
        {
            var color = await _colorSevice.GetColor(ColorId);
            if (color == null) { return NotFound(); }
            await _colorSevice.DeleteColor(ColorId);
            return NoContent();
        }
    }
}
