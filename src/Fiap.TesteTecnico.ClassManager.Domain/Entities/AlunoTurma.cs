namespace Fiap.TesteTecnico.ClassManager.Domain.Entities;
public class AlunoTurma
{
    public int AlunoId { get; set; }
    public virtual Aluno Aluno { get; set; }

    public int TurmaId { get; set; }
    public virtual Turma Turma { get; set; }
}
