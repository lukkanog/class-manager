using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunosTurmas;

public class GetAlunosTurmasQuery : IRequest<IEnumerable<AlunoTurmaDto>>
{
}
