namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;

public record AlunoDto(int Id, string Nome, string Usuario, IEnumerable<TurmaDto>? Turmas = default);
