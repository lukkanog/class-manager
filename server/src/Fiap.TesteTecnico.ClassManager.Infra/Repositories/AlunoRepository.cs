using Dapper;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories;
public class AlunoRepository(IDbConnectionFactory dbConnectionFactory) : IAlunoRepository
{
    private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;

    public async Task<IEnumerable<Aluno>> GetAllAsync()
    {
        const string sql = @"SELECT Id, Nome, Usuario, Senha FROM aluno";

        using var connection = _connectionFactory.Create();
        connection.Open();

        return await connection.QueryAsync<Aluno>(sql);
    }

    public async Task<Aluno> GetByIdAsync(int id)
    {
        const string sql = @"SELECT Id, Nome, Usuario, Senha FROM aluno WHERE id = @Id";

        using var connection = _connectionFactory.Create();
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<Aluno>(sql, new { Id = id });
    }

    public async Task<Aluno> GetByUsuarioAsync(string usuario)
    {
        const string sql = @"SELECT Nome, Usuario, Senha FROM aluno WHERE Usuario = @Usuario";

        using var connection = _connectionFactory.Create();
        connection.Open();

        return await connection.QueryFirstOrDefaultAsync<Aluno>(sql, new { Usuario = usuario });
    }

    public async Task<Aluno> AddAsync(Aluno aluno)
    {
        const string sql = @"
            INSERT INTO aluno (Nome, Usuario, Senha) 
            OUTPUT INSERTED.Id, INSERTED.Nome, INSERTED.Usuario, INSERTED.Senha
            VALUES (@Nome, @Usuario, @Senha)
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var createdAluno = await connection.QuerySingleAsync<Aluno>(sql, aluno);

        return createdAluno;
    }

    public async Task<Aluno> UpdateAsync(Aluno aluno)
    {
        const string sql = @"
            UPDATE aluno            
            SET Nome = @Nome, Usuario = @Usuario, Senha = @Senha
            OUTPUT INSERTED.Id, INSERTED.Nome, INSERTED.Usuario, INSERTED.Senha
            WHERE id = @Id
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var updatedAluno = await connection.QuerySingleOrDefaultAsync<Aluno>(sql, aluno);

        return updatedAluno;
    }

    public async Task DeleteAsync(int id)
    {
        const string sql = @"DELETE aluno WHERE id = @Id";

        using var connection = _connectionFactory.Create();
        connection.Open();

        await connection.ExecuteAsync(sql, new { Id = id});

        return;
    }
}
