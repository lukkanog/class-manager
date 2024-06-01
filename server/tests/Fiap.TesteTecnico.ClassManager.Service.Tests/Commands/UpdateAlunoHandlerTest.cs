using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateAluno;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(UpdateAlunoHandler))]
public class UpdateAlunoHandlerTest
{
    [Fact]
    public async Task Handle_InvalidRequest_ShouldNotUpdateAluno()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var validatorMock = new Mock<IValidator<UpdateAlunoCommand>>();

        var command = new UpdateAlunoCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new() } ));

        alunoRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(command);

        var handler = new UpdateAlunoHandler(alunoRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<ValidationFailureException>(() => handler.Handle(command, CancellationToken.None));
        alunoRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Aluno>()), Times.Never);
    }

    [Fact]
    public async Task Handle_AlunoNotFound_ShouldThrowNotFoundException()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var validatorMock = new Mock<IValidator<UpdateAlunoCommand>>();

        var command = new UpdateAlunoCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var handler = new UpdateAlunoHandler(alunoRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        alunoRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Aluno>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldUpdateAluno()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var validatorMock = new Mock<IValidator<UpdateAlunoCommand>>();

        var command = new UpdateAlunoCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var alunoFromRepository = new Aluno();
        alunoRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<Aluno>())).ReturnsAsync(new Aluno());
        alunoRepositoryMock.Setup(x => x.GetByIdAsync(command.Id)).ReturnsAsync(alunoFromRepository);

        var handler = new UpdateAlunoHandler(alunoRepositoryMock.Object, validatorMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(result.Id, alunoFromRepository.Id);
        alunoRepositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Aluno>()), Times.Once);
    }
}
