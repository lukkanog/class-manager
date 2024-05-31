using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;

namespace Fiap.TesteTecnico.ClassManager.Infra.Repositories;
public class TurmaRepository : ITurmaRepository
{
    public Task<Turma> AddAsync(Turma Turma) => throw new NotImplementedException();
    public Task DeleteAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<Turma>> GetAllAsync() => throw new NotImplementedException();
    public Task<Turma> GetByIdAsync(int id) => throw new NotImplementedException();
    public Task<Turma> GetByNameAsync(string name) => throw new NotImplementedException();
    public Task<Turma> UpdateAsync(Turma Turma) => throw new NotImplementedException();
}
