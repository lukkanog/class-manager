using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteTurma;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(DeleteTurmaHandler))]
public class DeleteTurmaHandlerTest
{
    [Fact]
    public async Task Handle_TurmaNotFound_ShouldThrowException()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();

        var id = 1;
        var command = new DeleteTurmaCommand(id);

        var handler = new DeleteTurmaHandler(turmaRepositoryMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        turmaRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldDeleteTurma()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();

        var id = 1;
        var command = new DeleteTurmaCommand(id);

        var turmaFromRepository = new Turma();
        turmaRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(turmaFromRepository);

        var handler = new DeleteTurmaHandler(turmaRepositoryMock.Object);

        await handler.Handle(command, CancellationToken.None);

        turmaRepositoryMock.Verify(x => x.DeleteAsync(id), Times.Once);
    }
}
