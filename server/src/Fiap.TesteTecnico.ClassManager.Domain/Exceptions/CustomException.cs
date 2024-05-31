namespace Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
public class CustomException : Exception
{
    public List<string> Errors { get; } = [];

    public CustomException() : base() { }

    public CustomException(string message) : base(message) { }

    public CustomException(string message, params object[] args) : base(string.Format(message, args)) { }
}
