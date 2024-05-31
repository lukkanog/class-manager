namespace Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
public class NotFoundException : CustomException
{
    public NotFoundException(string message) : base(message) { }
}
