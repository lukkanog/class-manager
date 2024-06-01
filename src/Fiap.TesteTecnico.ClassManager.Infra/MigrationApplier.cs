using DbUp;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Fiap.TesteTecnico.ClassManager.Infra;
public static class MigrationApplier
{
    public static void ApplyMigrations(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("ClassManager");

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader = DeployChanges.To
            .SqlDatabase(connectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .LogToConsole()
            .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception("Database migration failed", result.Error); 
        }
    }
}
