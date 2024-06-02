using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Extensions;
using Fiap.TesteTecnico.ClassManager.Service.Services;
using FluentValidation;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateAluno;

public class UpdateAlunoHandler(IAlunoRepository alunoRepository, IValidator<UpdateAlunoCommand> validator)
    : IRequestHandler<UpdateAlunoCommand, AlunoDto>
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    private readonly IValidator<UpdateAlunoCommand> _validator = validator;

    public async Task<AlunoDto> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
    { 
        var result = await _validator.ValidateAsync(request, cancellationToken);
        result.ThrowExceptionIfNotValid("Erro ao editar aluno");

        var existingAluno = await _alunoRepository.GetByIdAsync(request.Id);
        
        if (existingAluno is null)
            throw new NotFoundException($"Aluno de id {request.Id} não foi encontrado.");

        existingAluno.Nome = request.Nome;
        existingAluno.Usuario = request.Usuario;

        if (request.Senha is not null)
        {
            request.Senha = CriptographyService.HashPassword(request.Senha);
            existingAluno.Senha = request.Senha;
        }

        var updatedAluno = await _alunoRepository.UpdateAsync(existingAluno);

        return new AlunoDto(updatedAluno.Id, updatedAluno.Nome, updatedAluno.Usuario);
    }
}
