using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAlunoTurma;

public class DeleteAlunoTurmaCommand(int alunoId, int turmaId) : IRequest
{
    public int AlunoId { get; set; } = alunoId;
    public int TurmaId { get; set; } = turmaId;
}
