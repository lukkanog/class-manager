using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunos;

public class GetAlunosQueryHandler(IAlunoRepository alunoRepository) : IRequestHandler<GetAlunosQuery, IEnumerable<AlunoDto>>
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public async Task<IEnumerable<AlunoDto>> Handle(GetAlunosQuery request, CancellationToken cancellationToken)
    {
        var alunos = await _alunoRepository.GetAllAsync();

        return alunos.Select(aluno => new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        ));
    }
}
