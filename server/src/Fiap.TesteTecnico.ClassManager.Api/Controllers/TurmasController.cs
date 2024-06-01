using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmaById;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class TurmasController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TurmaDto>>> GetTurmas()
        => Ok(await _sender.Send(new GetTurmasQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<TurmaDto>> GetTurmas(int id)
        => Ok(await _sender.Send(new GetTurmaByIdQuery(id)));

    [HttpPut]
    public async Task<ActionResult<TurmaDto>> UpdateTurma([FromBody] UpdateTurmaCommand command)
        => Ok(await _sender.Send(command));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTurma(int id)
    {
        await _sender.Send(new DeleteTurmaCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<TurmaDto>> AddTurma([FromBody] CreateTurmaCommand command)
    {
        var addedTurma = await _sender.Send(command);
        return CreatedAtAction(nameof(GetTurmas), new { id = addedTurma.Id }, addedTurma);
    }
}
