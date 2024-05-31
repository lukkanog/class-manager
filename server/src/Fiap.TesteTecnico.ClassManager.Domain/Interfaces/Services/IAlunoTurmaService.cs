using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface IAlunoTurmaService
{
    Task<IEnumerable<AlunoTurma>> GetAllAsync();
    Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId);
    Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId);
    Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma);
    Task DeleteAsync(int alunoId, int turmaId);
}
