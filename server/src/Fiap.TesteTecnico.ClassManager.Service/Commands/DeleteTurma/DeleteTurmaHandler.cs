using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteTurma;
public class DeleteTurmaHandler(ITurmaRepository turmaRepository) : IRequestHandler<DeleteTurmaCommand>
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task Handle(DeleteTurmaCommand request, CancellationToken cancellationToken)
    {
        var turmaExistente = await _turmaRepository.GetByIdAsync(request.Id);

        if (turmaExistente is null)
            throw new NotFoundException($"Turma de id {request.Id} não foi encontrada.");

        await _turmaRepository.DeleteAsync(request.Id);
    }
}
