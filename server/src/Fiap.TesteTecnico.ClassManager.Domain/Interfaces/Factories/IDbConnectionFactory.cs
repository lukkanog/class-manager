using System.Data;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
public interface IDbConnectionFactory
{
    IDbConnection Create();
}
