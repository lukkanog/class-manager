using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;
using FluentValidation.Results;
using FluentValidation;
using Moq;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;

namespace Fiap.TesteTecnico.ClassManager.Service.Tests.Commands;

[Trait("Unit", nameof(CreateAlunoTurmaHandler))]
public class CreateAlunoTurmaHandlerTest
{
    [Fact]
    public async Task Handle_InvalidRequest_ShouldNotAddAlunoTurma()
    {
        var alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoTurmaCommand>>();

        var command = new CreateAlunoTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new List<ValidationFailure> { new() }));

        var handler = new CreateAlunoTurmaHandler(
            alunoTurmaRepositoryMock.Object,
            alunoRepositoryMock.Object,
            turmaRepositoryMock.Object,
            validatorMock.Object
        );

        await Assert.ThrowsAsync<ValidationFailureException>(() => handler.Handle(command, CancellationToken.None));
        alunoTurmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AlunoTurma>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ValidRequest_ShouldAddAlunoTurma()
    {
        var alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoTurmaCommand>>();

        var command = new CreateAlunoTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        alunoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Aluno { Id = 1 });

        turmaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Turma { Id = 1 });

        var returned = new AlunoTurma { AlunoId = 1, TurmaId = 1 };

        alunoTurmaRepositoryMock.Setup(x => x.AddAsync(It.IsAny<AlunoTurma>()))
            .ReturnsAsync(returned);

        var handler = new CreateAlunoTurmaHandler(
            alunoTurmaRepositoryMock.Object,
            alunoRepositoryMock.Object,
            turmaRepositoryMock.Object,
            validatorMock.Object
        );

        var result = await handler.Handle(command, CancellationToken.None);

        Assert.Equal(returned.AlunoId, result.AlunoId);
        Assert.Equal(returned.TurmaId, result.TurmaId);
        alunoTurmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AlunoTurma>()), Times.Once);
    }

    [Fact]
    public async Task Handle_AlunoNotFound_ShouldThrowException()
    {
        var alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoTurmaCommand>>();

        var command = new CreateAlunoTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        alunoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Aluno)null);

        var handler = new CreateAlunoTurmaHandler(
            alunoTurmaRepositoryMock.Object,
            alunoRepositoryMock.Object,
            turmaRepositoryMock.Object,
            validatorMock.Object
        );

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        alunoTurmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AlunoTurma>()), Times.Never);
    }

    [Fact]
    public async Task Handle_TurmaNotFound_ShouldThrowException()
    {
        var alunoTurmaRepositoryMock = new Mock<IAlunoTurmaRepository>();
        var alunoRepositoryMock = new Mock<IAlunoRepository>();
        var turmaRepositoryMock = new Mock<ITurmaRepository>();
        var validatorMock = new Mock<IValidator<CreateAlunoTurmaCommand>>();

        var command = new CreateAlunoTurmaCommand();

        validatorMock.Setup(x => x.ValidateAsync(command, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        alunoRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(new Aluno { Id = 1 });

        turmaRepositoryMock.Setup(x => x.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Turma)null);

        var handler = new CreateAlunoTurmaHandler(
            alunoTurmaRepositoryMock.Object,
            alunoRepositoryMock.Object,
            turmaRepositoryMock.Object,
            validatorMock.Object
        );

        await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(command, CancellationToken.None));
        alunoTurmaRepositoryMock.Verify(x => x.AddAsync(It.IsAny<AlunoTurma>()), Times.Never);
    }
}
