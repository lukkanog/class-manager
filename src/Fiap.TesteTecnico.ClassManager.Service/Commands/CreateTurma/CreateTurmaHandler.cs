using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Extensions;
using FluentValidation;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;

public class CreateTurmaHandler(ITurmaRepository turmaRepository, IValidator<CreateTurmaCommand> validator)
    : IRequestHandler<CreateTurmaCommand, TurmaDto>
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;
    private readonly IValidator<CreateTurmaCommand> _validator = validator;

    public async Task<TurmaDto> Handle(CreateTurmaCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);

        result.ThrowExceptionIfNotValid("Erro ao criar turma");

        var createdTurma = await _turmaRepository.AddAsync(request);

        return new TurmaDto(createdTurma.Id, createdTurma.CursoId, createdTurma.Nome, createdTurma.Ano);
    }
}
