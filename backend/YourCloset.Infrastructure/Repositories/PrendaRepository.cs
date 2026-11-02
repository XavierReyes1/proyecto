using Microsoft.EntityFrameworkCore;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Infrastructure.Data;

namespace YourCloset.Infrastructure.Repositories;

public class PrendaRepository : IPrendaRepository {
    private readonly YourClosetDbContext _context;

    public PrendaRepository(YourClosetDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Prenda>> GetAllAsync() {
        return await _context.Prendas.ToListAsync();
    }

    public async Task<Prenda?> GetByIdAsync(int id) {
        return await _context.Prendas.FindAsync(id);
    }

    public async Task<IEnumerable<Prenda>> GetByCategoriaAsync(string categoria) {
        return await _context.Prendas
            .Where(p => p.Categoria == categoria)
            .ToListAsync();
    }

    public async Task<Prenda> AddAsync(Prenda prenda) {
        await _context.Prendas.AddAsync(prenda);
        await _context.SaveChangesAsync();
        return prenda;
    }

    // Nuevo: Método de Actualización
    public async Task<Prenda> UpdateAsync(Prenda prenda) {
        _context.Prendas.Update(prenda);
        await _context.SaveChangesAsync();
        return prenda;
    }

    // Nuevo: Método de Eliminación
    public async Task<bool> DeleteAsync(int id) {
        var prenda = await _context.Prendas.FindAsync(id);
        if (prenda == null) return false;

        _context.Prendas.Remove(prenda);
        await _context.SaveChangesAsync();
        return true;
    }
}