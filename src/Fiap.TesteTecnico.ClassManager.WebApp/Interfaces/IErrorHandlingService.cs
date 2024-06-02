namespace Fiap.TesteTecnico.ClassManager.WebApp.Interfaces;

public interface IErrorHandlingService
{
    Task HandleErrorAsync(HttpResponseMessage response);
}
