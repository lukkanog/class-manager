using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Extensions;
using FluentValidation;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;

public class UpdateTurmaHandler(ITurmaRepository turmaRepository, IValidator<UpdateTurmaCommand> validator)
    : IRequestHandler<UpdateTurmaCommand, TurmaDto>
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IValidator<UpdateTurmaCommand> _validator = validator;

    public async Task<TurmaDto> Handle(UpdateTurmaCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);

        result.ThrowExceptionIfNotValid("Erro ao criar turma");

        var turmaExistente = await _turmaRepository.GetByIdAsync(request.Id);

        if (turmaExistente is null)
            throw new NotFoundException($"Turma de id {request.Id} não foi encontrada.");

        var updatedTurma = await _turmaRepository.UpdateAsync(request);

        return new TurmaDto(updatedTurma.Id, updatedTurma.CursoId, updatedTurma.Nome, updatedTurma.Ano);
    }
}
