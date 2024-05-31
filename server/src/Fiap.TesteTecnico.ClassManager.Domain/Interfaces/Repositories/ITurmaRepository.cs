using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
public interface ITurmaRepository
{
    Task<IEnumerable<Turma>> GetAllAsync();
    Task<Turma> GetByIdAsync(int id);
    Task<Turma> GetByNomeAsync(string name);
    Task<Turma> AddAsync(Turma Turma);
    Task<Turma> UpdateAsync(Turma Turma);
    Task DeleteAsync(int id);
}
