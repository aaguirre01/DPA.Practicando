using DPA.Practicando.DOMAIN.Core.Entities;
using DPA.Practicando.DOMAIN.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPA.Practicando.DOMAIN.Infrastructure.Repositories
{
    public class ReservaRepository : IReservaRepository
    {
        private readonly ReservasDeportivasContext _context;
        public ReservaRepository(ReservasDeportivasContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Reservas>> GetAllReservasAsync()
        {
            return await _context.Reservas.Include(r => r.Cancha).ToListAsync();

        }
        public async Task<Reservas?> GetReservaByIdAsync(int id)
        {
            return await _context.Reservas.Include(r => r.Cancha).FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<Reservas> CreateReservaAsync(Reservas reservas)
        {
            _context.Reservas.Add(reservas);
            await _context.SaveChangesAsync();
            return reservas;
        }
        public async Task<bool> UpdateReservaAsync(int id, Reservas reservas)
        {
            var current = await _context.Reservas.FindAsync(id);
            if (current == null)
            {
                return false;
            }
            current.Fecha = reservas.Fecha;
            current.HoraInicio = reservas.HoraInicio;
            current.HoraFin = reservas.HoraFin;
            current.ClienteNombre = reservas.ClienteNombre;
            current.CanchaId = reservas.CanchaId;
            await _context.SaveChangesAsync();
            return true;

        }
        public async Task<bool> DeleteReservaAsync(int id)
        {
            var reservas = await _context.Reservas.FindAsync(id);
            if (reservas == null)
            {
                return false;
            }
            _context.Reservas.Remove(reservas);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
