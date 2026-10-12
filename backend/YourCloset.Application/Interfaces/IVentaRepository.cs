using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IVentaRepository {
    Task<IEnumerable<Venta>> GetAllAsync();
    Task<Venta> AddAsync(Venta venta);
}