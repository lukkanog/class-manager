namespace Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
public class ValidationException : CustomException
{
    public ValidationException(string message, params string[] errors) : base(message)
    {
        Errors.AddRange(errors);
    }
}
