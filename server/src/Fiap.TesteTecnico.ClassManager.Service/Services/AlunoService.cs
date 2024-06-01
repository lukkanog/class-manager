using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;
public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public async Task<IEnumerable<AlunoDto>> GetAllAsync()
    {
        var alunos = await _alunoRepository.GetAllAsync();

        return alunos.Select(aluno => new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        ));
    }

    public async Task<AlunoDto> GetByIdAsync(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);

        if (aluno is null)
            throw new NotFoundException($"Aluno de id {id} não foi encontrado.");

        return new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        );
    }
}
