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
}