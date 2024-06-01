namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;

public record TurmaDto(int Id, int CursoId, string Nome, int Ano, IEnumerable<AlunoDto>? Alunos = default);
