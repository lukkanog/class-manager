using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateAluno;

public class UpdateAlunoCommand : IRequest<AlunoDto>
{
    public int Id { get; set; }
    public string? Nome { get; set; } = string.Empty;
    public string? Usuario { get; set; } = string.Empty;
    public string? Senha { get; set; }
    public string? ConfirmaSenha { get; set; }

    public static implicit operator Aluno(UpdateAlunoCommand command)
        => new()
        {
            Id = command.Id,
            Nome = command.Nome,
            Usuario = command.Usuario,
            Senha = command.Senha,
        };
}
