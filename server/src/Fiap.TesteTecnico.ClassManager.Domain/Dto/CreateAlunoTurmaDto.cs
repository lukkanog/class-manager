using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class CreateAlunoTurmaDto
{
    public int AlunoId { get; set; }
    public int TurmaId { get; set; }

    public static implicit operator AlunoTurma(CreateAlunoTurmaDto dto)
        => new()
        {
            AlunoId = dto.AlunoId,
            TurmaId = dto.TurmaId,
        };
}
