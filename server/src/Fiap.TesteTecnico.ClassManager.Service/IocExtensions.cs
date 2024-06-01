using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Fiap.TesteTecnico.ClassManager.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.TesteTecnico.ClassManager.Service;
public static class IocExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IAlunoService, AlunoService>();
        services.AddScoped<ITurmaService, TurmaService>();
        services.AddScoped<IAlunoTurmaService, AlunoTurmaService>();
    }
}
