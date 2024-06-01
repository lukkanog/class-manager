using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmas;

public class GetTurmasQuery : IRequest<IEnumerable<TurmaDto>>
{
}
