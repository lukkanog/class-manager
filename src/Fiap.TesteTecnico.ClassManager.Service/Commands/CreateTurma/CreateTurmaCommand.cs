using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;
public class CreateTurmaCommand : IRequest<TurmaDto>
{
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }

    public static implicit operator Turma(CreateTurmaCommand command)
        => new()
        {
            CursoId = command.CursoId,
            Nome = command.Nome,
            Ano = command.Ano,
        };
}
