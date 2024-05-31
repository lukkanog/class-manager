using Dapper;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories;
public class AlunoRepository(IDbConnectionFactory dbConnectionFactory) : IAlunoRepository
{
    private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;
    public async Task<Aluno> AddAsync(Aluno aluno)
    {
        const string sql = @"
            INSERT INTO aluno (Nome, Usuario, Senha) 
            OUTPUT INSERTED.Id, INSERTED.Nome, INSERTED.Usuario, INSERTED.Senha
            VALUES (@Nome, @Usuario, @Senha)
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var created = await connection.QuerySingleAsync<Aluno>(sql, aluno);

        return created;
    }

    public Task DeleteAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<Aluno>> GetAllAsync() => throw new NotImplementedException();
    public Task<Aluno> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<Aluno> GetByUsernameAsync(string username) => throw new NotImplementedException();
    public Task<Aluno> UpdateAsync(Aluno aluno) => throw new NotImplementedException();
}
