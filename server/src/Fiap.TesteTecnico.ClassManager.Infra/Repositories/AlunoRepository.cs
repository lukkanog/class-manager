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
        const string sql = @"
            SELECT 
                a.Id, a.Nome, a.Usuario,
                t.id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano
            FROM aluno a
            LEFT JOIN aluno_turma at ON at.aluno_id = a.id
            LEFT JOIN turma t ON t.id = at.turma_id
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var alunoDictionary = new Dictionary<int, Aluno>();

        var result = await connection.QueryAsync<Aluno, Turma, Aluno>(
            sql,
            (aluno, turma) =>
            {
                if (!alunoDictionary.TryGetValue(aluno.Id, out var currentAluno))
                {
                    currentAluno = aluno;
                    currentAluno.Turmas = new List<Turma>();
                    alunoDictionary.Add(currentAluno.Id, currentAluno);
                }

                if (turma is not null && turma.Id != 0)
                {
                    currentAluno.Turmas.Add(turma);
                }

                return currentAluno;
            },
            splitOn: "id"
        );

        return result.Distinct();
    }

    public async Task<Aluno> GetByIdAsync(int id)
    {
        const string sql = @"
            SELECT 
                a.Id, a.Nome, a.Usuario,
                t.id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano
            FROM aluno a
            LEFT JOIN aluno_turma at ON at.aluno_id = a.id
            LEFT JOIN turma t ON t.id = at.turma_id
            WHERE a.Id = @Id
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var alunoDictionary = new Dictionary<int, Aluno>();

        var result = await connection.QueryAsync<Aluno, Turma, Aluno>(
            sql,
            (aluno, turma) =>
            {
                if (!alunoDictionary.TryGetValue(aluno.Id, out var currentAluno))
                {
                    currentAluno = aluno;
                    currentAluno.Turmas = new List<Turma>();
                    alunoDictionary.Add(currentAluno.Id, currentAluno);
                }

                if (turma is not null && turma.Id != 0)
                {
                    currentAluno.Turmas.Add(turma);
                }

                return currentAluno;
            },
            new { Id = id },
            splitOn: "id"
        );

        return result.FirstOrDefault();
    }

    public async Task<Aluno> GetByUsuarioAsync(string usuario)
    {
        const string sql = @"SELECT Id, Nome, Usuario  FROM aluno WHERE Usuario = @Usuario";

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

        await connection.ExecuteAsync(sql, new { Id = id });

        return;
    }
}
