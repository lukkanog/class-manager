using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;

public class CreateAlunoTurmaHandler(
    IAlunoTurmaRepository alunoTurmaRepository,
    IAlunoRepository alunoRepository,
    ITurmaRepository turmaRepository
) : IRequestHandler<CreateAlunoTurmaCommand, AlunoTurmaDto>
{
    private readonly IAlunoTurmaRepository _alunoTurmaRepository = alunoTurmaRepository;
    private readonly IAlunoRepository _alunoRepository = alunoRepository;
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<AlunoTurmaDto> Handle(CreateAlunoTurmaCommand request, CancellationToken cancellationToken)
    {
        var existingAluno = await _alunoRepository.GetByIdAsync(request.AlunoId);
        if (existingAluno is null)
            throw new NotFoundException($"Aluno de id {request.AlunoId} não foi encontrado.");

        var existingTurma = await _turmaRepository.GetByIdAsync(request.TurmaId);
        if (existingTurma is null)
            throw new NotFoundException($"Turma de id {request.TurmaId} não foi encontrado.");

        var created = await _alunoTurmaRepository.AddAsync(request);

        return new AlunoTurmaDto(created.AlunoId, created.TurmaId);
    }
}
