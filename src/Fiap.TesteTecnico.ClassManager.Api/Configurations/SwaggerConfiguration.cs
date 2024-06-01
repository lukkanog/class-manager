using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Api.Configurations;

public static class SwaggerConfiguration
{
    public static void AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "API de Gerenciamento de Alunos",
                Version = "v1",
                Description = "API para gerenciamento de alunos e suas respectivas turmas."
            });
        });
    }
}
