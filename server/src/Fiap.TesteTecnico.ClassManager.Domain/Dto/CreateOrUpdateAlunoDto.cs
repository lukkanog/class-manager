using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class CreateOrUpdateAlunoDto
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha { get; set; } = string.Empty;

    public static implicit operator Aluno(CreateOrUpdateAlunoDto dto)
    {
        var aluno = new Aluno
        {
            Nome = dto.Nome,
            Usuario = dto.Usuario,
            Senha = dto.Senha
        };

        if (dto.Id.HasValue)
        {
            aluno.Id = dto.Id.Value;
        }

        return aluno;
    }
}
