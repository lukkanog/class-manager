using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;
public class TurmaService(ITurmaRepository turmaRepository) : ITurmaService
{
    private readonly ITurmaRepository _turmaRepository = turmaRepository;

    public async Task<IEnumerable<TurmaDto>> GetAllAsync()
    {
        var turmas = await _turmaRepository.GetAllAsync();
        return turmas.Select(turma => new TurmaDto(turma.Id, turma.CursoId, turma.Nome, turma.Ano));
    }

    public async Task<TurmaDto> GetByIdAsync(int id)
    {
        var turma = await _turmaRepository.GetByIdAsync(id);

        if (turma is null)
            throw new NotFoundException($"Turma de id {id} não foi encontrada.");

        return new TurmaDto(turma.Id, turma.CursoId, turma.Nome, turma.Ano);
    }

    public async Task<TurmaDto> AddAsync(CreateTurmaDto turmaDto)
    {
        var turma = new Turma
        {
            CursoId = turmaDto.CursoId,
            Nome = turmaDto.Nome,
            Ano = turmaDto.Ano
        };

        var createdTurma = await _turmaRepository.AddAsync(turma);

        return new TurmaDto(createdTurma.Id, createdTurma.CursoId, createdTurma.Nome, createdTurma.Ano);
    }

    public async Task<TurmaDto> UpdateAsync(UpdateTurmaDto turmaDto)
    {
        var turmaExistente = await _turmaRepository.GetByIdAsync(turmaDto.Id);

        if (turmaExistente is null)
            throw new NotFoundException($"Turma de id {turmaDto.Id} não foi encontrada.");

        var turma = new Turma
        {
            Id = turmaDto.Id,
            CursoId = turmaDto.CursoId,
            Nome = turmaDto.Nome,
            Ano = turmaDto.Ano
        };

        var updatedTurma = await _turmaRepository.UpdateAsync(turma);

        return new TurmaDto(updatedTurma.Id, updatedTurma.CursoId, updatedTurma.Nome, updatedTurma.Ano);
    }

    public async Task DeleteAsync(int id)
    {
        var turmaExistente = await _turmaRepository.GetByIdAsync(id);

        if (turmaExistente is null)
            throw new NotFoundException($"Turma de id {id} não foi encontrada.");

        await _turmaRepository.DeleteAsync(id);
    }
}
