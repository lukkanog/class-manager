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
            const string sql = @"SELECT Id, curso_id AS CursoId, turma AS Nome, Ano FROM turma";

            using var connection = _connectionFactory.Create();
            connection.Open();

            return await connection.QueryAsync<Turma>(sql);
        }

        public async Task<Turma> GetByIdAsync(int id)
        {
            const string sql = @"SELECT Id, curso_id AS CursoId, turma AS Nome, Ano FROM turma WHERE id = @Id";

            using var connection = _connectionFactory.Create();
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<Turma>(sql, new { Id = id });
        }

        public async Task<Turma> GetByNomeAsync(string nome)
        {
            const string sql = @"SELECT Id, curso_id AS CursoId, turma AS Nome, Ano FROM turma WHERE Nome = @Nome";

            using var connection = _connectionFactory.Create();
            connection.Open();

            return await connection.QueryFirstOrDefaultAsync<Turma>(sql, new { Nome = nome });
        }

        public async Task<Turma> AddAsync(Turma turma)
        {
            const string sql = @"
                INSERT INTO turma (CursoId, turma AS Nome, Ano) 
                OUTPUT INSERTED.Id, INSERTED.curso_id AS CursoId, INSERTED.turma AS turma AS Nome, INSERTED.Ano
                VALUES (@CursoId, @turma AS Nome, @Ano)
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
                SET CursoId = @CursoId, Nome = @turma AS Nome, Ano = @Ano
                OUTPUT INSERTED.Id, INSERTED.CursoId, INSERTED.turma AS Nome, INSERTED.Ano
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
