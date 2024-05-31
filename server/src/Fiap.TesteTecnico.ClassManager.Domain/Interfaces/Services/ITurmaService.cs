using Fiap.TesteTecnico.ClassManager.Domain.Dto;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface ITurmaService
{
    Task<IEnumerable<TurmaDto>> GetAllAsync();
    Task<TurmaDto> GetByIdAsync(int id);
    Task<TurmaDto> AddAsync(CreateTurmaDto turmaDto);
    Task<TurmaDto> UpdateAsync(UpdateTurmaDto turmaDto);
    Task DeleteAsync(int id);
}
