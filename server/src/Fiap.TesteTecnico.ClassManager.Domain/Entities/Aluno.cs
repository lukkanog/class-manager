﻿using Fiap.TesteTecnico.ClassManager.Domain.ValueObjects;

namespace Fiap.TesteTecnico.ClassManager.Domain.Entities;

public class Aluno
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Usuario { get; set; } = string.Empty;
    public Password Senha { get; set; } = string.Empty;
}
