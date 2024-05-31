using Fiap.TesteTecnico.ClassManager.Domain.Dto;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto> GetByIdAsync(Guid id);
    Task<AlunoDto> AddAsync(CreateOrUpdateAlunoDto alunoDto);
    Task<AlunoDto> UpdateAsync(CreateOrUpdateAlunoDto alunoDto);
    Task DeleteAsync(int id);
}
