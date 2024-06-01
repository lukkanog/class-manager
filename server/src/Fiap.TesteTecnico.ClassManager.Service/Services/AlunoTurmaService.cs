using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;

public class AlunoTurmaService(IAlunoTurmaRepository alunoTurmaRepository) : IAlunoTurmaService
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;

    public async Task<AlunoTurmaDto> AddAsync(CreateAlunoTurmaDto alunoTurma)
    {
        var created = await _alunoTurmaRepository.AddAsync(alunoTurma);

        return new AlunoTurmaDto(created.AlunoId, created.TurmaId);
    }

    public async Task<IEnumerable<AlunoTurmaDto>> GetAllAsync()
    {
        var alunoTurmas = await _alunoTurmaRepository.GetAllAsync();

        return alunoTurmas
            .Select(x => new AlunoTurmaDto(
                x.AlunoId, x.TurmaId, 
                new AlunoDto(x.Aluno.Id, x.Aluno.Nome, x.Aluno.Usuario), 
                new TurmaDto(x.Turma.Id, x.Turma.CursoId, x.Turma.Nome, x.Turma.Ano)
            ));
    }

    public async Task DeleteAsync(int alunoId, int turmaId)
    {
        var alunoTurmaExistente = await _alunoTurmaRepository.GetByAlunoIdAndTurmaIdAsync(alunoId, turmaId);

        if (alunoTurmaExistente is null)
            throw new NotFoundException($"Não foi encontrada uma relação de aluno de id {alunoId} com a turma {turmaId}");

        await _alunoTurmaRepository.DeleteAsync(alunoId, turmaId);
    }
}
