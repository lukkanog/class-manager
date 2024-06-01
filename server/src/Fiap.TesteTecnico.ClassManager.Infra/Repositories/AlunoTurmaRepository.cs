using Dapper;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories;
public class AlunoTurmaRepository(IDbConnectionFactory dbConnectionFactory) : IAlunoTurmaRepository
{
    private readonly IDbConnectionFactory _connectionFactory = dbConnectionFactory;
    public async Task<IEnumerable<AlunoTurma>> GetAllAsync()
    {
        const string sql = @"
                SELECT at.aluno_id AS AlunoId, at.turma_id AS TurmaId, a.Id, a.Nome, a.Usuario, t.Id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano
                FROM aluno_turma at
                INNER JOIN aluno a ON at.aluno_id = a.Id
                INNER JOIN turma t ON at.turma_id = t.Id
        ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        return await connection.QueryAsync<AlunoTurma, Aluno, Turma, AlunoTurma>(
            sql,
            (alunoTurma, aluno, turma) =>
            {
                alunoTurma.Aluno = aluno;
                alunoTurma.Turma = turma;
                return alunoTurma;
            },
            splitOn: "Id,Id"
        );
    }

    public async Task<AlunoTurma> GetByAlunoIdAndTurmaIdAsync(int alunoId, int turmaId)
    {
        const string sql = @"
                SELECT at.aluno_id AS AlunoId, at.turma_id AS TurmaId, 
                       a.Id, a.Nome, a.Usuario, 
                       t.Id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano
                FROM aluno_turma at
                INNER JOIN aluno a ON at.aluno_id = a.Id
                INNER JOIN turma t ON at.turma_id = t.Id
                WHERE at.aluno_id = @AlunoId AND at.turma_id = @TurmaId
            ";

        using var connection = _connectionFactory.Create();
        connection.Open();

        var alunoTurmas = await connection.QueryAsync<AlunoTurma, Aluno, Turma, AlunoTurma>(
            sql,
            (alunoTurma, aluno, turma) =>
            {
                alunoTurma.Aluno = aluno;
                alunoTurma.Turma = turma;
                return alunoTurma;
            },
            new { AlunoId = alunoId, TurmaId = turmaId },
            splitOn: "Id,Id"
        );

        return alunoTurmas.FirstOrDefault();
    }

    public async Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma)
    {
        const string sql = @"INSERT INTO aluno_turma (aluno_id, turma_id) VALUES (@AlunoId, @TurmaId)";

        using var connection = _connectionFactory.Create();
        connection.Open();

        await connection.ExecuteAsync(sql, alunoTurma);

        return alunoTurma;
    }

    public async Task DeleteAsync(int alunoId, int turmaId)
    {
        const string sql = @"DELETE FROM aluno_turma WHERE aluno_id = @AlunoId AND turma_id = @TurmaId";

        using var connection = _connectionFactory.Create();
        connection.Open();

        await connection.ExecuteAsync(sql, new { AlunoId = alunoId, TurmaId = turmaId });
    }
}
