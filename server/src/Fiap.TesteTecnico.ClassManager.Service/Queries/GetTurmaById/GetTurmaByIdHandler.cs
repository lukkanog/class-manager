using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmaById;

public class GetTurmaByIdHandler(ITurmaRepository turmaRepository) : IRequestHandler<GetTurmaByIdQuery, TurmaDto>
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<TurmaDto> Handle(GetTurmaByIdQuery request, CancellationToken cancellationToken)
    {
        var turma = await _turmaRepository.GetByIdAsync(request.Id);

        if (turma is null)
            throw new NotFoundException($"Turma de id {request.Id} não foi encontrada.");

        return new TurmaDto(
            turma.Id,
            turma.CursoId,
            turma.Nome,
            turma.Ano,
            turma.Alunos.Select(a => new AlunoDto(a.Id, a.Nome, a.Usuario))
        );
    }
}
