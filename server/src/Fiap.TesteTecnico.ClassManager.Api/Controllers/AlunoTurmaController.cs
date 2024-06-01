using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAlunoTurma;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunosTurmas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class AlunoTurmaController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoTurmaDto>>> GetAllAlunoTurma()
        => Ok(await _sender.Send(new GetAlunosTurmasQuery()));

    [HttpPost]
    public async Task<ActionResult<AlunoTurmaDto>> AddAlunoTurma([FromBody] CreateAlunoTurmaCommand command)
    {
        var alunoTurma = await _sender.Send(command);
        return Ok(alunoTurma);
    }

    [HttpDelete("aluno/{alunoId}/turma/{turmaId}")]
    public async Task<ActionResult> DeleteAlunoTurma(int alunoId, int turmaId)
    {
        await _sender.Send(new DeleteAlunoTurmaCommand(alunoId, turmaId));
        return NoContent();
    }
}
