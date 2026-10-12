using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IPrendaRepository {
    Task<IEnumerable<Prenda>> GetAllAsync();
    Task<Prenda> AddAsync(Prenda prenda);
}