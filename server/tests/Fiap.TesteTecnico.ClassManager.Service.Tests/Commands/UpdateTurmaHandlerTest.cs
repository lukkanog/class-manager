using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(UpdateTurmaHandler))]
public class UpdateTurmaHandlerTest
{
    [Fact]
    public async Task Handle_InvalidRequest_ShouldNotUpdateTurma()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<UpdateTurmaCommand>>();

        var command = new UpdateTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new() } ));

        turmaRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(command);

        var handler = new UpdateTurmaHandler(turmaRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<ValidationFailureException>(() => handler.Handle(command, CancellationToken.None));
        turmaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Turma>()), Times.Never);
    }

    [Fact]
    public async Task Handle_TurmaNotFound_ShouldThrowNotFoundException()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<UpdateTurmaCommand>>();

        var command = new UpdateTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var handler = new UpdateTurmaHandler(turmaRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        turmaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Turma>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldUpdateTurma()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<UpdateTurmaCommand>>();

        var command = new UpdateTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var turmaFromRepository = new Turma();
        turmaRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Turma>())).ReturnsAsync(new Turma());
        turmaRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(turmaFromRepository);

        var handler = new UpdateTurmaHandler(turmaRepositoryMock.Object, validatorMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(result.Id, turmaFromRepository.Id);
        turmaRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Turma>()), Times.Once);
    }
}
