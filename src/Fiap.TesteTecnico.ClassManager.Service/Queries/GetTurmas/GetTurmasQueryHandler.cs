using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmas;

public class GetTurmasQueryHandler(ITurmaRepository turmaRepository) : IRequestHandler<GetTurmasQuery, IEnumerable<TurmaDto>>
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<IEnumerable<TurmaDto>> Handle(GetTurmasQuery request, CancellationToken cancellationToken)
    {
        var turmas = await _turmaRepository.GetAllAsync();

        return turmas.Select(turma => new TurmaDto(
            turma.Id,
            turma.CursoId,
            turma.Nome,
            turma.Ano,
            turma.Alunos.Select(a => new AlunoDto(a.Id, a.Nome, a.Usuario))
        ));
    }
}
