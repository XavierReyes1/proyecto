using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace YourCloset.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        
        // Escanea y registra todos los validadores automáticamente
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}