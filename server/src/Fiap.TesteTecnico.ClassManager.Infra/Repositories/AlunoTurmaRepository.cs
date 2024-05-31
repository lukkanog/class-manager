using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories;
public class AlunoTurmaRepository : IAlunoTurmaRepository
{
    public Task<AlunoTurma> AddAsync(AlunoTurma alunoTurma) => throw new NotImplementedException();
    public Task DeleteAsync(int alunoId, int turmaId) => throw new NotImplementedException();
    public Task<IEnumerable<AlunoTurma>> GetAllAsync() => throw new NotImplementedException();
    public Task<AlunoTurma> GetByAlunoIdAndTurmaIdAsync(int alunoId, int turmaId) => throw new NotImplementedException();
    public Task<IEnumerable<AlunoTurma>> GetByAlunoIdAsync(int alunoId) => throw new NotImplementedException();
    public Task<IEnumerable<AlunoTurma>> GetByTurmaIdAsync(int turmaId) => throw new NotImplementedException();
}
