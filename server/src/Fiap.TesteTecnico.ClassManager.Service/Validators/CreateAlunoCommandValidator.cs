using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.Aluno.CreateAluno;
using FluentValidation;

namespace Fiap.TesteTecnico.ClassManager.Service.Validators;

public class CreateAlunoCommandValidator : AbstractValidator<CreateAlunoCommand>
{
    private readonly IAlunoRepository _alunoRepository;

    public CreateAlunoCommandValidator(IAlunoRepository alunoRepository)
    {
        _alunoRepository = alunoRepository;

        RuleFor(x => x.Senha)
            .Equal(x => x.ConfirmaSenha)
            .WithMessage("Senha e confirmação de senha devem ser iguais.")
            .MinimumLength(6)
            .WithMessage("Senha deve ter ao mínimo 6 caracteres")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{6,}$")
            .WithMessage("Senha deve conter pelo menos uma letra maiúscula, uma letra minúscula, um dígito e um caractere especial.");

        RuleFor(x => x.Usuario)
            .MaximumLength(45)
            .WithMessage("Aluno deve ter no máximo 45 caracteres.")
            .MustAsync(async (usuario, cancellationToken) =>
            {
                var usuarioExistente = await _alunoRepository.GetByUsuarioAsync(usuario);
                return usuarioExistente is null;
            })
            .WithMessage("Aluno já existe.");

        RuleFor(x => x.Nome)
            .MaximumLength(255)
            .WithMessage("Nome deve ter no máximo 255 caracteres.");

    }
}
