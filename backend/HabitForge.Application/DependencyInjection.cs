using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace HabitForge.Application;

public static class DependencyInjection {
    public static IServiceCollection AddApplication(this IServiceCollection services) {
        
        // Escanea y registra todos los validadores que existan en esta capa automáticamente
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        
        return services;
    }
}