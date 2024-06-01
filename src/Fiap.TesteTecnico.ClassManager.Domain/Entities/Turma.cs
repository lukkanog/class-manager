namespace Fiap.TesteTecnico.ClassManager.Domain.Entities;
public class Turma
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }

    public virtual List<Aluno> Alunos { get; set; }
}
