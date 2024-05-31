using Fiap.TesteTecnico.ClassManager.Domain.Dto.Aluno;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto> GetByIdAsync(Guid id);
    Task<AlunoDto> AddAsync(CreateOrUpdateAlunoDto aluno);
    Task<AlunoDto> UpdateAsync(CreateOrUpdateAlunoDto aluno);
    Task DeleteAsync(int id);
}
