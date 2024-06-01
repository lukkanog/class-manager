using Dapper;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Factories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories
{
    public class TurmaRepository : ITurmaRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TurmaRepository(IDbConnectionFactory dbConnectionFactory)
        {
            _connectionFactory = dbConnectionFactory;
        }

        public async Task<IEnumerable<Turma>> GetAllAsync()
        {
            const string sql = @"
                SELECT 
                    t.Id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano,
                    a.Id, a.Nome, a.Usuario
                FROM turma t
                LEFT JOIN aluno_turma at ON at.turma_id = t.id
                LEFT JOIN aluno a ON a.id = at.aluno_id
            ";

            using var connection = _connectionFactory.Create();
            connection.Open();

            var turmaDictionary = new Dictionary<int, Turma>();

            var result = await connection.QueryAsync<Turma, Aluno, Turma>(
                sql,
                (turma, aluno) =>
                {
                    if (!turmaDictionary.TryGetValue(turma.Id, out var currentTurma))
                    {
                        currentTurma = turma;
                        currentTurma.Alunos = new List<Aluno>();
                        turmaDictionary.Add(currentTurma.Id, currentTurma);
                    }

                    if (aluno is not null && aluno.Id != 0)
                    {
                        currentTurma.Alunos.Add(aluno);
                    }

                    return currentTurma;
                },
                splitOn: "Id"
            );

            return result.Distinct();
        }

        public async Task<Turma> GetByIdAsync(int id)
        {
            const string sql = @"
                SELECT 
                    t.Id, t.curso_id AS CursoId, t.turma AS Nome, t.Ano,
                    a.Id, a.Nome, a.Usuario
                FROM turma t
                LEFT JOIN aluno_turma at ON at.turma_id = t.id
                LEFT JOIN aluno a ON a.id = at.aluno_id
                WHERE t.id = @Id
            ";

            using var connection = _connectionFactory.Create();
            connection.Open();

            var turmaDictionary = new Dictionary<int, Turma>();

            var result = await connection.QueryAsync<Turma, Aluno, Turma>(
                sql,
                (turma, aluno) =>
                {
                    if (!turmaDictionary.TryGetValue(turma.Id, out var currentTurma))
                    {
                        currentTurma = turma;
                        currentTurma.Alunos = new List<Aluno>();
                        turmaDictionary.Add(currentTurma.Id, currentTurma);
                    }

                    if (aluno is not null && aluno.Id != 0)
                    {
                        currentTurma.Alunos.Add(aluno);
                    }

                    return currentTurma;
                },
                new { Id = id },
                splitOn: "Id"
            );

            return result.FirstOrDefault();
        }

        public async Task<Turma> GetByNomeAsync(string nome)
        {
            const string sql = @"SELECT Id, curso_id AS CursoId, turma AS Nome, Ano FROM turma WHERE turma = @Nome";

            using var connection = _connectionFactory.Create();
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<Turma>(sql, new { Nome = nome });
        }

        public async Task<Turma> AddAsync(Turma turma)
        {
            const string sql = @"
                INSERT INTO turma (curso_id, turma, ano) 
                OUTPUT INSERTED.Id, INSERTED.curso_id AS CursoId, INSERTED.turma AS Nome, INSERTED.Ano
                VALUES (@CursoId, @Nome, @Ano)
            ";

            using var connection = _connectionFactory.Create();
            connection.Open();

            var createdTurma = await connection.QuerySingleAsync<Turma>(sql, turma);

            return createdTurma;
        }

        public async Task<Turma> UpdateAsync(Turma turma)
        {
            const string sql = @"
                UPDATE turma
                SET curso_id = @CursoId, turma = @Nome, ano = @Ano
                OUTPUT INSERTED.Id, INSERTED.curso_id AS CursoId, INSERTED.turma AS Nome, INSERTED.Ano
                WHERE id = @Id
            ";

            using var connection = _connectionFactory.Create();
            connection.Open();

            var updatedTurma = await connection.QuerySingleOrDefaultAsync<Turma>(sql, turma);

            return updatedTurma;
        }

        public async Task DeleteAsync(int id)
        {
            const string sql = @"DELETE FROM turma WHERE id = @Id";

            using var connection = _connectionFactory.Create();
            connection.Open();

            await connection.ExecuteAsync(sql, new { Id = id });
        }
    }
}
