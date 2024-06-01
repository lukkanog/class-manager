using Fiap.TesteTecnico.ClassManager.Domain.Dto;
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

    /// <summary>
    /// Retorna uma lista de todos os alunos e suas respectivas turmas.
    /// </summary>
    /// <returns>Uma lista de objetos de aluno.</returns>
    /// <response code="200">Retorna a lista de alunos.</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AlunoDto>>> GetAlunos()
        => Ok(await _sender.Send(new GetAlunosQuery()));

    /// <summary>
    /// Retorna os detalhes de um aluno específico pelo Id.
    /// </summary>
    /// <param name="id">Id do aluno.</param>
    /// <returns>O aluno encontrado.</returns>
    /// <response code="200">Retorna o aluno encontrado.</response>
    /// <response code="404">Se o aluno não for encontrado.</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<AlunoDto>> GetAluno(int id)
        => Ok(await _sender.Send(new GetAlunoByIdQuery(id)));

    /// <summary>
    /// Atualiza os dados de um aluno existente.
    /// </summary>
    /// <param name="command">Objeto contendo os dados atualizados do aluno.</param>
    /// <returns>O aluno que foi atualizado.</returns>
    /// <response code="200">Retorna o aluno que foi atualizado.</response>
    /// <response code="400">Se os dados fornecidos forem inválidos.</response>
    /// <response code="404">Se o aluno não for encontrado.</response>
    [HttpPut]
    public async Task<ActionResult<AlunoDto>> UpdateAluno([FromBody] UpdateAlunoCommand command)
        => Ok(await _sender.Send(command));

    /// <summary>
    /// Exclui um aluno pelo Id.
    /// </summary>
    /// <param name="id">O Id do aluno a ser excluído.</param>
    /// <returns>NoContent indicando que o aluno foi excluído com sucesso.</returns>
    /// <response code="204">Indica que o aluno foi excluído com sucesso.</response>
    /// <response code="404">Se o aluno não for encontrado.</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAluno(int id)
    {
        await _sender.Send(new DeleteAlunoCommand(id));
        return NoContent();
    }

    /// <summary>
    /// Adiciona um novo aluno.
    /// </summary>
    /// <param name="command">Objeto contendo os dados do novo aluno.</param>
    /// <returns>O aluno que foi criado.</returns>
    /// <response code="201">Retorna o aluno que foi criado.</response>
    /// <response code="400">Se os dados fornecidos forem inválidos.</response>
    [ProducesResponseType(typeof(AlunoDto), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<ActionResult<AlunoDto>> AddAluno([FromBody] CreateAlunoCommand command)
    {
        var addedAluno = await _sender.Send(command);
        return CreatedAtAction(nameof(GetAlunos), new { id = addedAluno.Id }, addedAluno);
    }
}