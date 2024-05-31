using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Fiap.TesteTecnico.ClassManager.Infra;
public static class IocExtensions
{
    public static void AddConnectionFactory(this IServiceCollection services)
    {
        services.AddSingleton<IDbConnectionFactory, SqlServerConnectionFactory>();
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IAlunoRepository, AlunoRepository>();
        services.AddScoped<ITurmaRepository, TurmaRepository>();
        services.AddScoped<IAlunoTurmaRepository, AlunoTurmaRepository>();
    }
}
