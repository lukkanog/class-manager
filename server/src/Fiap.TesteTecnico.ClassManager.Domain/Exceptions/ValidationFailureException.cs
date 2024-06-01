namespace Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
public class ValidationFailureException : CustomException
{
    public ValidationFailureException(string message, params string[] errors) : base(message)
    {
        Errors.AddRange(errors);
    }
}
