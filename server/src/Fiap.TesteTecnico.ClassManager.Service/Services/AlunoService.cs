using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Entities;
using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Repositories;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;

namespace Fiap.TesteTecnico.ClassManager.Service.Services;
public class AlunoService(IAlunoRepository alunoRepository) : IAlunoService
{
    private readonly IAlunoRepository _alunoRepository = alunoRepository;

    public async Task<AlunoDto> AddAsync(CreateAlunoDto alunoDto)
    {
        var aluno = await _alunoRepository.AddAsync(alunoDto);

        return new AlunoDto(aluno.Id, aluno.Nome, aluno.Usuario);
    }
    public async Task<IEnumerable<AlunoDto>> GetAllAsync()
    {
        var alunos = await _alunoRepository.GetAllAsync();

        return alunos.Select(aluno => new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        ));
    }

    public async Task<AlunoDto> GetByIdAsync(int id)
    {
        var aluno = await _alunoRepository.GetByIdAsync(id);

        if (aluno is null)
            throw new NotFoundException($"Aluno de id {id} não foi encontrado.");

        return new AlunoDto(
            aluno.Id,
            aluno.Nome,
            aluno.Usuario,
            aluno.Turmas.Select(t => new TurmaDto(t.Id, t.CursoId, t.Nome, t.Ano))
        );
    }

    public async Task<AlunoDto> UpdateAsync(UpdateAlunoDto alunoDto)
    {
        var alunoExistente = await _alunoRepository.GetByIdAsync(alunoDto.Id);

        if (alunoExistente is null)
            throw new NotFoundException($"Aluno de id {alunoDto.Id} não foi encontrado.");

        var aluno = await _alunoRepository.UpdateAsync(alunoDto);

        return new AlunoDto(aluno.Id, aluno.Nome, aluno.Usuario);
    }

    public async Task DeleteAsync(int id)
    {
        var alunoExistente = await _alunoRepository.GetByIdAsync(id);

         if (alunoExistente is null)
            throw new NotFoundException($"Aluno de id {id} não foi encontrado.");

        await _alunoRepository.DeleteAsync(id);
    }
}
