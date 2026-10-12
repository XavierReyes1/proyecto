using Microsoft.EntityFrameworkCore;
using YourCloset.Application.Interfaces;
using YourCloset.Domain.Entities;
using YourCloset.Infrastructure.Data;

namespace YourCloset.Infrastructure.Repositories;

public class ClienteRepository : IClienteRepository {
    private readonly YourClosetDbContext _context;

    public ClienteRepository(YourClosetDbContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Cliente>> GetAllAsync() {
        return await _context.Clientes.ToListAsync();
    }

    public async Task<Cliente?> GetByIdAsync(int id) {
        return await _context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> AddAsync(Cliente cliente) {
        await _context.Clientes.AddAsync(cliente);
        await _context.SaveChangesAsync();
        return cliente;
    }
}