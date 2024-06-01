using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAluno;

public class DeleteAlunoHandler(IAlunoRepository alunoRepository) : IRequestHandler<DeleteAlunoCommand>
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public async Task Handle(DeleteAlunoCommand request, CancellationToken cancellationToken)
    {
        var existingAluno = await _alunoRepository.GetByIdAsync(request.Id);

        if (existingAluno is null)
            throw new NotFoundException($"Aluno de id {request.Id} não foi encontrado.");

        await _alunoRepository.DeleteAsync(request.Id);
    }
}
