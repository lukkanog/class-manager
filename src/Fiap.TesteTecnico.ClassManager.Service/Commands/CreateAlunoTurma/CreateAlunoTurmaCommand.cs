using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;

public class CreateAlunoTurmaCommand : IRequest<AlunoTurmaDto>
{
    public CreateAlunoTurmaCommand()
    {
    }

    public CreateAlunoTurmaCommand(int alunoId, int turmaId)
    {
        AlunoId = alunoId;
        TurmaId = turmaId;
    }

    public int AlunoId { get; set; }
    public int TurmaId { get; set; }

    public static implicit operator AlunoTurma(CreateAlunoTurmaCommand command)
        => new()
        {
            AlunoId = command.AlunoId,
            TurmaId = command.TurmaId,
        };
}
