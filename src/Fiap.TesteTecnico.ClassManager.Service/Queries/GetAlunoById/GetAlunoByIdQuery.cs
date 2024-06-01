using Fiap.TesteTecnico.ClassManager.Domain.Dto;
using MediatR;

namespace Fiap.TesteTecnico.ClassManager.Service.Queries.GetAlunoById;

public class GetAlunoByIdQuery(int id) : IRequest<AlunoDto>
{
    public int Id { get; set; } = id;
}
