using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateAluno;
using FluentValidation;

namespace Fiap.TesteTecnico.ClassManager.Service.Validators;

public class UpdateAlunoCommandValidator : AbstractValidator<UpdateAlunoCommand>
{
    private readonly IAlunoRepository _alunoRepository;

    public UpdateAlunoCommandValidator(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;

        When(x => x.Senha is not null, () =>
        {
            RuleFor(x => x.Senha)
            .Equal(x => x.ConfirmaSenha)
            .WithMessage("Senha e confirmação de senha devem ser iguais.")
            .MinimumLength(6)
            .WithMessage("Senha deve ter ao mínimo 6 caracteres")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{6,}$")
            .WithMessage("Senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um dígito e um caractere especial.");
        });

        RuleFor(x => x.Usuario)
            .MaximumLength(45)
            .WithMessage("Aluno deve ter no máximo 45 caracteres.");

        RuleFor(x => new { x.Usuario, x.Id })
            .MustAsync(async (model, _, cancellationToken) =>
            {
                var existingUsuario = await _alunoRepository.GetByUsuarioAsync(model.Usuario);
                return existingUsuario is null || existingUsuario.Id == model.Id;
            })
            .WithMessage("Aluno já existe.");

        RuleFor(x => x.Nome)
            .MaximumLength(255)
            .WithMessage("Nome deve ter no máximo 255 caracteres.");
    }
}
