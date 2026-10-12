using Microsoft.EntityFrameworkCore;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Infrastructure.Data;

namespace YourCloset.Infrastructure.Repositories;

public class VentaRepository : IVentaRepository {
    private readonly YourClosetDbContext _context;

    public VentaRepository(YourClosetDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Venta>> GetAllAsync() {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Prenda)
            .ToListAsync();
    }

    public async Task<Venta?> GetByIdAsync(int id) {
        return await _context.Ventas
            .Include(v => v.Cliente)
            .Include(v => v.Prenda)
            .FirstOrDefaultAsync(v => v.Id == id);
    }

    public async Task<IEnumerable<Venta>> GetByClienteAsync(int clienteId) {
        return await _context.Ventas
            .Include(v => v.Prenda)
            .Where(v => v.ClienteId == clienteId)
            .ToListAsync();
    }

    public async Task<Venta> AddAsync(Venta venta) {
        await _context.Ventas.AddAsync(venta);
        await _context.SaveChangesAsync();
        return venta;
    }
}