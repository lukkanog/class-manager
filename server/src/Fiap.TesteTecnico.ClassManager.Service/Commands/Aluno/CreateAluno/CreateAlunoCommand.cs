using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.Aluno.CreateAluno;

public class CreateAlunoCommand : IRequest<AlunoDto>
{
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha { get; set; } = string.Empty;

    public static implicit operator Aluno(CreateAlunoCommand command)
        => new()
        {
            Nome = command.Nome,
            Usuario = command.Usuario,
            Senha = command.Senha
        };
}
