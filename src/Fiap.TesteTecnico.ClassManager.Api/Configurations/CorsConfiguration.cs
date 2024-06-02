namespace Fiap.TesteTecnico.ClassManager.Api.Configurations;

public static class CorsConfiguration
{
    public static void AddCorsDefaultPolicy(this IServiceCollection services, string policyName)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(policyName, policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });
    }
}
