using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetTurmaById;

public class GetTurmaByIdQuery(int id) : IRequest<TurmaDto>
{
    public int Id { get; set; } = id;
}
