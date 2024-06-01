using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;
using FluentValidation;

namespace Fiap.TesteTecnico.ClassManager.Service.Validators;
public class CreateAlunoTurmaCommandValidator
    : AbstractValidator<CreateAlunoTurmaCommand>
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository;

    public CreateAlunoTurmaCommandValidator(IAlunoTurmaRepository alunoTurmaRepository)
    {
        _alunoTurmaRepository = alunoTurmaRepository;

        RuleFor(x => new { x.AlunoId, x.TurmaId })
            .MustAsync(async (model, _, cancellationToken) =>
            {
                var existing = await _alunoTurmaRepository.GetByAlunoIdAndTurmaIdAsync(model.AlunoId, model.TurmaId);
                return existing is null;
            })
            .WithMessage("Aluno já está relacionado a essa turma");
    }

}
