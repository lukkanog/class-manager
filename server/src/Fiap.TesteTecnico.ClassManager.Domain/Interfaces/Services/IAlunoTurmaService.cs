using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface IAlunoTurmaService
{
    Task<IEnumerable<AlunoTurmaDto>> GetAllAsync();
    Task<AlunoTurmaDto> AddAsync(CreateAlunoTurmaDto alunoTurma);
    Task DeleteAsync(int alunoId, int turmaId);
}
