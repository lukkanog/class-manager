﻿namespace Fiap.TesteTecnico.ClassManager.Domain.Dto;
public class CreateTurmaDto
{
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }
}