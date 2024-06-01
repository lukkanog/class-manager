using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(CreateTurmaHandler))]
public class CreateTurmaHandlerTest
{
    [Fact]
    public async Task Handle_InvalidRequest_ShouldNotAddTurma()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateTurmaCommand>>();

        var command = new CreateTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new() } ));

        var handler = new CreateTurmaHandler(turmaRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<ValidationFailureException>(() => handler.Handle(command, CancellationToken.None));
        turmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Turma>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldAddTurma()
    {
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateTurmaCommand>>();

        var command = new CreateTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var turmaFromRepository = new Turma();
        turmaRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Turma>())).ReturnsAsync(new Turma());

        var handler = new CreateTurmaHandler(turmaRepositoryMock.Object, validatorMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(result.Id, turmaFromRepository.Id);
        turmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Turma>()), Times.Once);
    }
}
