using DPA.Practicando.DOMAIN.Core.Entities;

namespace DPA.Practicando.DOMAIN.Infrastructure.Repositories
{
    public interface IReservaRepository
    {
        Task<Reservas> CreateReservaAsync(Reservas reservas);
        Task<bool> DeleteReservaAsync(int id);
        Task<IEnumerable<Reservas>> GetAllReservasAsync();
        Task<Reservas?> GetReservaByIdAsync(int id);
        Task<bool> UpdateReservaAsync(int id, Reservas reservas);
    }
}