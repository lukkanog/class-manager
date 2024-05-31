using Fiap.TesteTecnico.ClassManager.Domain.Dto.Turma;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
public interface ITurmaService
{
    Task<IEnumerable<Turma>> GetAllAsync();
    Task<Turma> GetByIdAsync(Guid id);
    Task<Turma> AddAsync(CreateOrUpdateTurmaDto turma);
    Task<Turma> UpdateAsync(CreateOrUpdateTurmaDto turma);
    Task DeleteAsync(int id);
}
