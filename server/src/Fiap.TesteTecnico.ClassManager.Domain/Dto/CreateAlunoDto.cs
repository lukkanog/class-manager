using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class CreateAlunoDto
{
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha { get; set; } = string.Empty;

    public static implicit operator Aluno(CreateAlunoDto dto)
        => new()
        {
            Nome = dto.Nome,
            Usuario = dto.Usuario,
            Senha = dto.Senha
        };
}
