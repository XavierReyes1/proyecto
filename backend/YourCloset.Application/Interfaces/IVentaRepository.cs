using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IVentaRepository {
    Task<IEnumerable<Venta>> GetAllAsync();
    Task<Venta?> GetByIdAsync(int id);
    Task<IEnumerable<Venta>> GetByClienteAsync(int clienteId);
    Task<Venta> AddAsync(Venta venta);
    Task<Venta> UpdateAsync(Venta venta); 
    Task<bool> DeleteAsync(int id);      
}