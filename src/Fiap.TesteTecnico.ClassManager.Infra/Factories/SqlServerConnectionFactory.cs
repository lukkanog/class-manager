using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Fiap.TesteTecnico.ClassManager.Infra.Factories;
public class SqlServerConnectionFactory(IConfiguration configuration) : IDbConnectionFactory
{
    private readonly string _connectionString = configuration.GetConnectionString("ClassManager");
    public IDbConnection Create()
    {
        if (string.IsNullOrEmpty(_connectionString))
        {
            throw new ArgumentNullException(
                nameof(_connectionString),
                "Connection string is required to create connections to SQL Server"
            );
        }

        return new SqlConnection(_connectionString);
    }
}
