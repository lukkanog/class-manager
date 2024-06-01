using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunos;

public class GetAlunosQuery : IRequest<IEnumerable<AlunoDto>>
{
}
