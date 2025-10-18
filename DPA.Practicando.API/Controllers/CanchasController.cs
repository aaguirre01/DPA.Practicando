using DPA.Practicando.DOMAIN.Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DPA.Practicando.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CanchasController : ControllerBase
    {
        private readonly ReservasDeportivasContext _context;
        public CanchasController(ReservasDeportivasContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCanchas()
        {
            var canchas = await _context.Canchas.ToListAsync();
            return Ok(canchas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCanchaById(int id)
        {
            var cancha = await _context.Canchas.FindAsync(id);
            if (cancha == null)
            {
                return NotFound();
            }
            return Ok(cancha);
        }
        [HttpPost]
        public async Task<IActionResult> CreateCancha([FromBody] Canchas canchas)
        {
            if (canchas == null)
            
                return BadRequest();
            _context.Canchas.Add(canchas);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCanchaById), new { id = canchas.Id }, canchas);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCancha(int id)
        {
            var canchas = await _context.Canchas.FindAsync(id);
            if (canchas == null)
            {
                return NotFound();
            }
            _context.Canchas.Remove(canchas);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCancha(int id, [FromBody] Canchas canchas)
        {
           if (id != canchas.Id)
            {
                return BadRequest();
            }
            var existingCancha = await _context.Canchas.FindAsync(id);
            if (existingCancha == null)
            {
                return NotFound();
            }
            existingCancha.Nombre = canchas.Nombre;
            existingCancha.Tipo = canchas.Tipo;
            existingCancha.Ubicacion = canchas.Ubicacion;
            _context.Canchas.Update(existingCancha);
            await _context.SaveChangesAsync();
            return NoContent();

        }





    }   
        
}
