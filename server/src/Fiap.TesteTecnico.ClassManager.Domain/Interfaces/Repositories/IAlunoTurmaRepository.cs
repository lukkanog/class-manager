using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
public interface IAlunoTurmaRepository
{
    Task<IEnumerable<AlunoTurma>> GetAllAsync();
    Task<AlunoTurma> GetByAlunoIdAndTurmaIdAsync(int alunoId, int turmaId);
    Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma);
    Task DeleteAsync(int alunoId, int turmaId);
}
