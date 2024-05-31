using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class TurmasController(ITurmaService service) : ControllerBase
{
    private readonly ITurmaService _service = service;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TurmaDto>>> GetTurmas()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<ActionResult<TurmaDto>> GetTurmas(int id)
        => Ok(await _service.GetByIdAsync(id));

    [HttpPut]
    public async Task<ActionResult<TurmaDto>> UpdateTurma([FromBody] UpdateTurmaDto turmaDto)
        => Ok(await _service.UpdateAsync(turmaDto));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTurma(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TurmaDto>> AddTurma([FromBody] CreateTurmaDto turmaDto)
    {
        var addedTurma = await _service.AddAsync(turmaDto);
        return CreatedAtAction(nameof(GetTurmas), new { id = addedTurma.Id }, addedTurma);
    }
}
