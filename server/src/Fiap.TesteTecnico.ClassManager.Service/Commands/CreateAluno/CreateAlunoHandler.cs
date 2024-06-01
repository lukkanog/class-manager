using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Extensions;
using FluentValidation;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAluno;

public class CreateAlunoHandler(IAlunoRepository alunoRepository, IValidator<CreateAlunoCommand> validator)
    : IRequestHandler<CreateAlunoCommand, AlunoDto>
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    private readonly IValidator<CreateAlunoCommand> _validator = validator;

    public async Task<AlunoDto> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
    {
        var result = await _validator.ValidateAsync(request, cancellationToken);

        result.ThrowExceptionIfNotValid("Erro ao criar aluno");

        var alunoCriado = await _alunoRepository.AddAsync(request);

        return new AlunoDto(alunoCriado.Id, alunoCriado.Nome, alunoCriado.Usuario);
    }
}
