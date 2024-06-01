using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Service.Commands.CreateAlunoTurma;
using Fiap.TesteTecnico.ClassManager.Service.Commands.DeleteAlunoTurma;
using Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunosTurmas;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class AlunoTurmaController : ControllerBase
    {
        private readonly ISender _sender;

        public AlunoTurmaController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Retorna uma lista de todos os registros de alunos em turmas.
        /// </summary>
        /// <returns>Uma lista de objetos de AlunoTurma.</returns>
        /// <response code="200">Retorna a lista de registros de alunos em turmas.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AlunoTurmaDto>>> GetAllAlunoTurma()
            => Ok(await _sender.Send(new GetAlunosTurmasQuery()));

        /// <summary>
        /// Adiciona um novo registro de aluno em turma.
        /// </summary>
        /// <param name="command">Objeto contendo os dados do novo registro de aluno em turma.</param>
        /// <returns>O registro de aluno em turma que foi criado.</returns>
        /// <response code="200">Retorna o registro de aluno em turma que foi criado.</response>
        /// <response code="400">Se os dados fornecidos forem inválidos.</response>
        [HttpPost]
        public async Task<ActionResult<AlunoTurmaDto>> AddAlunoTurma([FromBody] CreateAlunoTurmaCommand command)
        {
            var alunoTurma = await _sender.Send(command);
            return Ok(alunoTurma);
        }

        /// <summary>
        /// Exclui um registro de aluno em turma pelo Id do aluno e Id da turma.
        /// </summary>
        /// <param name="alunoId">O Id do aluno.</param>
        /// <param name="turmaId">O Id da turma.</param>
        /// <returns>NoContent indicando que o registro de aluno em turma foi excluído com sucesso.</returns>
        /// <response code="204">Indica que o registro de aluno em turma foi excluído com sucesso.</response>
        /// <response code="404">Se o registro de aluno em turma não for encontrado.</response>
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpDelete("aluno/{alunoId}/turma/{turmaId}")]
        public async Task<ActionResult> DeleteAlunoTurma(int alunoId, int turmaId)
        {
            await _sender.Send(new DeleteAlunoTurmaCommand(alunoId, turmaId));
            return NoContent();
        }
    }
}
