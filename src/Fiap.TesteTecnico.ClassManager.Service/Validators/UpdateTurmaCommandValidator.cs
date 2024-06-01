using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;
using FluentValidation;

namespace Fiap.TesteTecnico.ClassManager.Service.Validators;
public class UpdateTurmaCommandValidator : AbstractValidator<UpdateTurmaCommand>
{
    private readonly ITurmaRepository _turmaRepository;

    public UpdateTurmaCommandValidator(ITurmaRepository turmaRepository)
    {
        _turmaRepository = turmaRepository;

        RuleFor(x => x.Ano)
            .Must(x => x.ToString().Length == 4)
            .WithMessage("Ano deve ter 4 caracteres (yyyy)");

        RuleFor(x => x.Nome)
            .MaximumLength(45)
            .WithMessage("Nome da turma deve ter no máximo 45 caracteres.");

        RuleFor(x => new { x.Nome, x.Id })
            .MustAsync(async (model, _, cancellationToken) =>
            {
                var existingNome = await _turmaRepository.GetByNomeAsync(model.Nome);
                return existingNome is null || existingNome.Id == model.Id;
            })
            .WithMessage("Aluno já existe.");

    }
}
