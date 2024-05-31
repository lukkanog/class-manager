using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
public interface IAlunoTurmaRepository
{
    Task<IEnumerable<AlunoTurma>> GetAllAsync();
    Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId);
    Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId);
    Task<AlunoTurma> GetByAlunoIdAndTurmaIdAsync(int alunoId, int turmaId);
    Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma);
    Task DeleteAsync(int alunoId, int turmaId);
}
