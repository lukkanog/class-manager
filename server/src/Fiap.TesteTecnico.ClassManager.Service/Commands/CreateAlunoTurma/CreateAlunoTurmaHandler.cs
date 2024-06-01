using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;

public class CreateAlunoTurmaHandler(IAlunoTurmaRepository alunoTurmaRepository) : IRequestHandler<CreateAlunoTurmaCommand, AlunoTurmaDto>
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;

    public async Task<AlunoTurmaDto> Handle(CreateAlunoTurmaCommand request, CancellationToken cancellationToken)
    {
        var created = await _alunoTurmaRepository.AddAsync(request);

        return new AlunoTurmaDto(created.AlunoId, created.TurmaId);
    }
}
