using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using Fiap.TesteTecnico.ClassManager.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Fiap.TesteTecnico.ClassManager.Api.Controllers;

[Produces("application/json")]
[Route("v1/[controller]")]
public class AlunosController(IAlunoService service) : ControllerBase
{
    private readonly IAlunoService _service = service;

    [HttpPost]
    public async Task<ActionResult<AlunoDto>> AddAluno([FromBody] CreateOrUpdateAlunoDto alunoDto)
        => Ok(await _service.AddAsync(alunoDto));
}
