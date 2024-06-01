using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;
public class UpdateTurmaCommand : IRequest<TurmaDto>
{
    public int Id { get; set; }
    public int CursoId { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ano { get; set; }

    public static implicit operator Turma(UpdateTurmaCommand command)
        => new()
        {
            Id = command.Id,
            CursoId = command.CursoId,
            Nome = command.Nome,
            Ano = command.Ano,
        };
}
