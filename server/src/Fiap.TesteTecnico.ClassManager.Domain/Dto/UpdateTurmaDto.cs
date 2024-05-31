namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class UpdateTurmaDto
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
}
