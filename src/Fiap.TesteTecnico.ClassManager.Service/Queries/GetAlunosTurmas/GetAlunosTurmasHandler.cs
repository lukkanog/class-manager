using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunosTurmas;
public class GetAlunosTurmasHandler(IAlunoTurmaRepository alunoTurmaRepository) 
    : IRequestHandler<GetAlunosTurmasQuery, IEnumerable<AlunoTurmaDto>>
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;

    public async Task<IEnumerable<AlunoTurmaDto>> Handle(GetAlunosTurmasQuery request, CancellationToken cancellationToken)
    {
        var alunoTurmas = await _alunoTurmaRepository.GetAllAsync();

        return alunoTurmas
            .Select(x => new AlunoTurmaDto(
                x.AlunoId, x.TurmaId,
                new AlunoDto(x.Aluno.Id, x.Aluno.Nome, x.Aluno.Usuario),
                new TurmaDto(x.Turma.Id, x.Turma.CursoId, x.Turma.Nome, x.Turma.Ano)
            ));
    }
}
