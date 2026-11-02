using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IClienteRepository {
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente?> GetByIdAsync(int id);
    Task<Cliente> AddAsync(Cliente cliente);
    Task<Cliente> UpdateAsync(Cliente cliente); 
    Task<bool> DeleteAsync(int id);             
}