using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAluno;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(CreateAlunoHandler))]
public class CreateAlunoHandlerTest
{
    [Fact]
    public async Task Handle_InvalidRequest_ShouldNotAddAluno()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoCommand>>();

        var command = new CreateAlunoCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new() } ));

        var handler = new CreateAlunoHandler(alunoRepositoryMock.Object, validatorMock.Object);

        await Assert.ThrowsAsync<ValidationFailureException>(() => handler.Handle(command, CancellationToken.None));
        alunoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Aluno>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldAddAluno()
    {
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoCommand>>();

        var command = new CreateAlunoCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var alunoFromRepository = new Aluno();
        alunoRepositoryMock.Setup(x => x.AddAsync(It.IsAny<Aluno>())).ReturnsAsync(new Aluno());

        var handler = new CreateAlunoHandler(alunoRepositoryMock.Object, validatorMock.Object);

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(result.Id, alunoFromRepository.Id);
        alunoRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Aluno>()), Times.Once);
    }
}
