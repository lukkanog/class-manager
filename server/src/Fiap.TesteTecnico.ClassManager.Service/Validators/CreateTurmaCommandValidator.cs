using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;
using FluentValidation;

namespace Fiap.TesteTecnico.ClassManager.Service.Validators;
public class CreateTurmaCommandValidator : AbstractValidator<CreateTurmaCommand>
{
    private readonly ITurmaRepository _turmaRepository;

    public CreateTurmaCommandValidator(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;

        RuleFor(x => x.Ano)
            .Must(x => x.ToString().Length == 4)
            .WithMessage("Ano deve ter 4 caracteres (yyyy)");

        RuleFor(x => x.Nome)
            .MaximumLength(45)
            .WithMessage("Nome da turma deve ter no máximo 45 caracteres.")
            .MustAsync(async (nome, cancellationToken) =>
            {
                var turmaExistente = await _turmaRepository.GetByNomeAsync(nome);
                return turmaExistente is null;
            })
            .WithMessage("Turma já existe.");

    }
}
