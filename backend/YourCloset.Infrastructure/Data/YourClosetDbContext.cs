using Microsoft.EntityFrameworkCore;
using YourCloset.Domain.Entities;

namespace YourCloset.Infrastructure.Data;

public class YourClosetDbContext : DbContext {
    public YourClosetDbContext(DbContextOptions<YourClosetDbContext> options) : base(options) { }

    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Prenda> Prendas { get; set; }
    public DbSet<Venta> Ventas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);

        // Cliente → Ventas (1:N)
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Cliente)
            .WithMany(c => c.Ventas)
            .HasForeignKey(v => v.ClienteId);

        // Prenda → Ventas (1:N)
        modelBuilder.Entity<Venta>()
            .HasOne(v => v.Prenda)
            .WithMany(p => p.Ventas)
            .HasForeignKey(v => v.PrendaId);

        // Precisión decimal para precios
        modelBuilder.Entity<Prenda>()
            .Property(p => p.Precio)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Venta>()
            .Property(v => v.Total)
            .HasPrecision(10, 2);
    }
}