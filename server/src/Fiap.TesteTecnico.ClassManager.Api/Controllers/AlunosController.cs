﻿using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAluno;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAluno;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateAluno;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunos;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunoById;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class AlunosController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
        => Ok(await _sender.Send(new GetAlunosQuery()));

    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> GetAluno(int id)
        => Ok(await _sender.Send(new GetAlunoByIdQuery(id)));

    [HttpPut]
    public async Task<ActionResult<AlunoDto>> UpdateAluno([FromBody] UpdateAlunoCommand command)
        => Ok(await _sender.Send(command));

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAluno(int id)
    {
        await _sender.Send(new DeleteAlunoCommand(id));
        return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> AddAluno([FromBody] CreateAlunoCommand command)
    {
        var addedAluno = await _sender.Send(command);
        return CreatedAtAction(nameof(GetAlunos), new { id = addedAluno.Id }, addedAluno);
    }
}
