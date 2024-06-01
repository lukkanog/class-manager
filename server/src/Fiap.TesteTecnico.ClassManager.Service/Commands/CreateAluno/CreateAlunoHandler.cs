using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Service.Extensions;
using FluentValidation;
using MediatR;
using System.Security.Cryptography;
using System.Text;

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

        request.Senha = HashPassword(request.Senha);

        var alunoCriado = await _alunoRepository.AddAsync(request);

        return new AlunoDto(alunoCriado.Id, alunoCriado.Nome, alunoCriado.Usuario);
    }

    private static string HashPassword(string password)
    {
        byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        StringBuilder builder = new StringBuilder();

        for (int i = 0; i < bytes.Length; i++)
        {
            builder.Append(bytes[i].ToString("x2"));
        }

        return builder.ToString();
    }
}
