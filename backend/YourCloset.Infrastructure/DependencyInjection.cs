using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using YourCloset.Application.Interfaces;
using YourCloset.Infrastructure.Data;
using YourCloset.Infrastructure.Repositories;

namespace YourCloset.Infrastructure;

public static class DependencyInjection {
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString) {

        // 1. Configuramos el acceso a PostgreSQL
        services.AddDbContext<YourClosetDbContext>(options =>
            options.UseNpgsql(connectionString));

        // 2. LA INYECCIÓN CLAVE (Los contratos firmados por el Gerente):
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IPrendaRepository, PrendaRepository>();
        services.AddScoped<IVentaRepository, VentaRepository>();

        return services;
    }
}