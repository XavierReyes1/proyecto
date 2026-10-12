using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IClienteRepository {
    Task<IEnumerable<Cliente>> GetAllAsync();
    Task<Cliente> AddAsync(Cliente cliente);
}