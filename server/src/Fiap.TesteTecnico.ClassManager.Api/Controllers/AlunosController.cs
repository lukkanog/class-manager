using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class AlunosController(IAlunoService service) : ControllerBase
{
    private readonly IAlunoService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> GetAlunos(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPut]
    public async Task<ActionResult<AlunoDto>> UpdateAluno([FromBody] UpdateAlunoDto alunoDto)
        => Ok(await _service.UpdateAsync(alunoDto));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAluno(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> AddAluno([FromBody] CreateAlunoDto alunoDto)
    {
        var addedAluno = await _service.AddAsync(alunoDto);
        return CreatedAtAction(nameof(GetAlunos), new { id = addedAluno.Id }, addedAluno);
    }
}
