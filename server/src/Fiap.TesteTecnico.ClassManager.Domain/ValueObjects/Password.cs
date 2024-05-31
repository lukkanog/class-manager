namespace Fiap.TesteTecnico.ClassManager.Domain.ValueObjects;
public class Password
{
    public string Value { get; set; } = string.Empty;

    public static implicit operator Password(string value) 
        => new() { Value = value };

    public static implicit operator string(Password password)
        => password.Value;
}
