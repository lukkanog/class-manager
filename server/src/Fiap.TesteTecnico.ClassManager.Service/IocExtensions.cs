using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Fiap.TesteTecnico.ClassManager.Service.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Service;
public static class IocExtensions
{
    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<ITurmaService, TurmaService>();
        services.AddScoped<IAlunoTurmaService, AlunoTurmaService>();
    }

    public static void AddValidators(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    }
}
