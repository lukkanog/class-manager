using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAlunoTurma;

public class DeleteAlunoTurmaHandler(IAlunoTurmaRepository alunoTurmaRepository) : IRequestHandler<DeleteAlunoTurmaCommand>
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;

    public async Task Handle(DeleteAlunoTurmaCommand request, CancellationToken cancellationToken)
    {
        var alunoexistingTurma = await _alunoTurmaRepository.GetByAlunoIdAndTurmaIdAsync(request.AlunoId, request.TurmaId);
        if (alunoexistingTurma is null)
            throw new NotFoundException($"Não foi encontrada uma relação de aluno de id {request.AlunoId} com a turma {request.TurmaId}");

        await _alunoTurmaRepository.DeleteAsync(request.AlunoId, request.TurmaId);
    }
}
