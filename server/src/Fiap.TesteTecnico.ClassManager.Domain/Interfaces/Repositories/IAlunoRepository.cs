using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
public interface IAlunoRepository
{
    Task<IEnumerable<Aluno>> GetAllAsync();
    Task<Aluno> GetByIdAsync(int id);
    Task<Aluno> GetByUsernameAsync(string username);
    Task<Aluno> AddAsync(Aluno aluno);
    Task<Aluno> UpdateAsync(Aluno aluno);
    Task DeleteAsync(int id);
}
