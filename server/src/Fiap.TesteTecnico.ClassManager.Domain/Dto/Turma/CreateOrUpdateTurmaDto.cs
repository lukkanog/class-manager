﻿namespace Fiap.TesteTecnico.ClassManager.Domain.Dto.Turma;
public class CreateOrUpdateTurmaDto
{
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
}