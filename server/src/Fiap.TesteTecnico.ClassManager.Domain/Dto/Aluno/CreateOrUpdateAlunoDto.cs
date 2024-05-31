namespace Fiap.TesteTecnico.ClassManager.Domain.Dto.Aluno;
public class CreateOrUpdateAlunoDto
{
    public int? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string ConfirmaSenha { get; set; } = string.Empty;
}
