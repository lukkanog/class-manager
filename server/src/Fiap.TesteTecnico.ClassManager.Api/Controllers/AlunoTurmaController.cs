using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class AlunoTurmaController(IAlunoTurmaService alunoTurmaService) : ControllerBase
{
    private readonly IAlunoTurmaService _service = alunoTurmaService;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoTurmaDto>>> GetAllAlunoTurma()
        => Ok(await _service.GetAllAsync());

    [HttpPost]
    public async Task<ActionResult<AlunoTurmaDto>> AddAlunoTurma([FromBody] CreateAlunoTurmaDto alunoTurma)
        => Ok(await _service.AddAsync(alunoTurma));

    [HttpDelete("aluno/{alunoId}/turma/{turmaId}")]
    public async Task<ActionResult> DeleteAlunoTurma(int alunoId, int turmaId)
    {
        await _service.DeleteAsync(alunoId, turmaId);
        return NoContent();
    }
}
