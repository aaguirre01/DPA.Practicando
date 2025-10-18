using DPA.Practicando.DOMAIN.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DPA.Practicando.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;
        public ReservasController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;

        }
        [HttpGet]
        public async Task<IActionResult> GetAllReservas()
        {
            var reservas = await _reservaRepository.GetAllReservasAsync();
            return Ok(reservas);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReservaById(int id)
        {
            var reserva = await _reservaRepository.GetReservaByIdAsync(id);
            if (reserva == null)
            {
                return NotFound();
            }
            return Ok(reserva);
        }
        [HttpPost]
        public async Task<IActionResult> CreateReserva([FromBody] DOMAIN.Core.Entities.Reservas reservas)
        {
            if (reservas == null)
                return BadRequest();
            var createdReserva = await _reservaRepository.CreateReservaAsync(reservas);
            return CreatedAtAction(nameof(GetReservaById), new { id = createdReserva.Id }, createdReserva);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReserva(int id, [FromBody] DOMAIN.Core.Entities.Reservas reservas)
        {
            if (id != reservas.Id)
            {
                return BadRequest();
            }
            var result = await _reservaRepository.UpdateReservaAsync(id, reservas);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReserva(int id)
        {
            var result = await _reservaRepository.DeleteReservaAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
    
}