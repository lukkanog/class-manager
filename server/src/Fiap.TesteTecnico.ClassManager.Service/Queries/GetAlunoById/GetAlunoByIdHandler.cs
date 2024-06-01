using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunoById;

public class GetAlunoByIdHandler(IAlunoRepository alunoRepository) : IRequestHandler<GetAlunoByIdQuery, AlunoDto>
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    public async Task<AlunoDto> Handle(GetAlunoByIdQuery request, CancellationToken cancellationToken)
    {
        var aluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (aluno is null)
            throw new NotFoundException($"Aluno de id {request.Id} não foi encontrado.");

        return new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        );
    }
}
