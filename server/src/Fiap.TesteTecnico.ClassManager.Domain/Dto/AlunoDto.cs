namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class AlunoDto(int id, string nome, string usuario)
{
    public int Id { get; set; } = id;
    public string Nome { get; set; } = nome;
    public string Usuario { get; set; } = usuario;
}
