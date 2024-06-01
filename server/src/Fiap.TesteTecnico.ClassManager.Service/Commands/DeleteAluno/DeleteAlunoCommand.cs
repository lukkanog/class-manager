using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAluno;

public class DeleteAlunoCommand(int id) : IRequest
{
    public int Id { get; set; } = id;
}
