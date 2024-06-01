using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAluno;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(DeleteAlunoHandler))]
public class DeleteAlunoHandlerTest
{
    [Fact]
    public async Task Handle_AlunoNotFound_ShouldThrowException()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();

        var id = 1;
        var command = new DeleteAlunoCommand(id);

        var handler = new DeleteAlunoHandler(alunoRepositoryMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        alunoRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldDeleteAluno()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();

        var id = 1;
        var command = new DeleteAlunoCommand(id);

        var alunoFromRepository = new Aluno();
        alunoRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(alunoFromRepository);

        var handler = new DeleteAlunoHandler(alunoRepositoryMock.Object);

        await handler.Handle(command, CancellationToken.None);

        alunoRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
    }
}
