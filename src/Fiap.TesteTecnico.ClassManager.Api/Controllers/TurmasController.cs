using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.UpdateTurma;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmaById;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class TurmasController : ControllerBase
    {
        private readonly ISender _sender;

        public TurmasController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retorna uma lista de todas as turmas.
        /// </summary>
        /// <returns>Uma lista de turmas.</returns>
        /// <response code="200">Retorna a lista de turmas.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurmaDto>>> GetTurmas()
            => Ok(await _sender.Send(new GetTurmasQuery()));

        /// <summary>
        /// Retorna os detalhes de uma turma específica pelo Id.
        /// </summary>
        /// <param name="id">Id da turma.</param>
        /// <returns>A turma encontrada.</returns>
        /// <response code="200">Retorna a turma encontrada.</response>
        /// <response code="404">Se a turma não for encontrada.</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<TurmaDto>> GetTurmas(int id)
            => Ok(await _sender.Send(new GetTurmaByIdQuery(id)));

        /// <summary>
        /// Atualiza os dados de uma turma existente.
        /// </summary>
        /// <param name="command">Objeto contendo os dados atualizados da turma.</param>
        /// <returns>A turma que foi atualizada.</returns>
        /// <response code="200">Retorna a turma que foi atualizada.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        /// <response code="404">Se a turma não for encontrada.</response>
        [HttpPut]
        public async Task<ActionResult<TurmaDto>> UpdateTurma([FromBody] UpdateTurmaCommand command)
            => Ok(await _sender.Send(command));

        /// <summary>
        /// Exclui uma turma pelo Id.
        /// </summary>
        /// <param name="id">O Id da turma a ser excluída.</param>
        /// <returns>NoContent indicando que a turma foi excluída com sucesso.</returns>
        /// <response code="204">Indica que a turma foi excluída com sucesso.</response>
        /// <response code="404">Se a turma não for encontrada.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTurma(int id)
        {
            await _sender.Send(new DeleteTurmaCommand(id));
            return NoContent();
        }

        /// <summary>
        /// Adiciona uma nova turma.
        /// </summary>
        /// <param name="command">Objeto contendo os dados da nova turma.</param>
        /// <returns>A turma que foi criada.</returns>
        /// <response code="201">Retorna a turma que foi criada.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [ProducesResponseType(typeof(TurmaDto), StatusCodes.Status201Created)]
        [HttpPost]
        public async Task<ActionResult<TurmaDto>> AddTurma([FromBody] CreateTurmaCommand command)
        {
            var addedTurma = await _sender.Send(command);
            return CreatedAtAction(nameof(GetTurmas), new { id = addedTurma.Id }, addedTurma);
        }
    }
}
