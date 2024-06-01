using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteTurma;

public class DeleteTurmaCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
