using Fiap.TesteTecnico.ClassManager.Domain.Dto;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface IAlunoService
{
    Task<IEnumerable<AlunoDto>> GetAllAsync();
    Task<AlunoDto> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}
