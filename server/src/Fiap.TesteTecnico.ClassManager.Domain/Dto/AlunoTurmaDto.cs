namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;

public record AlunoTurmaDto(int AlunoId, int TurmaId, AlunoDto? Aluno = default, TurmaDto? Turma = default);
