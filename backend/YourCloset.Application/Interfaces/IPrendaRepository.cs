using YourCloset.Domain.Entities;

namespace YourCloset.Application.Interfaces;

public interface IPrendaRepository {
    Task<IEnumerable<Prenda>> GetAllAsync();
    Task<Prenda?> GetByIdAsync(int id);
    Task<IEnumerable<Prenda>> GetByCategoriaAsync(string categoria);
    Task<Prenda> AddAsync(Prenda prenda);
    Task<Prenda> UpdateAsync(Prenda prenda); 
    Task<bool> DeleteAsync(int id);          
}