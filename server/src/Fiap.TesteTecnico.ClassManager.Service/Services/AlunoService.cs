using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;
public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    public async Task<AlunoDto> AddAsync(CreateOrUpdateAlunoDto alunoDto)
    {
        var aluno = await _alunoRepository.AddAsync(alunoDto);

        return new AlunoDto(aluno.Id, aluno.Nome, aluno.Usuario);
    }

    public Task DeleteAsync(int id) => throw new NotImplementedException();
    public Task<IEnumerable<AlunoDto>> GetAllAsync() => throw new NotImplementedException();
    public Task<AlunoDto> GetByIdAsync(Guid id) => throw new NotImplementedException();
    public Task<AlunoDto> UpdateAsync(CreateOrUpdateAlunoDto alunoDto) => throw new NotImplementedException();
}
