using Fiap.TesteTecnico.ClassManager.WebApp.Interfaces;
using Fiap.TesteTecnico.ClassManager.WebApp.Models;
using MudBlazor;
using System.Net.Http.Json;

namespace Fiap.TesteTecnico.ClassManager.WebApp.Services;

public class ErrorHandlingService : IErrorHandlingService
{
    private readonly ISnackbar _snackbar;

    public ErrorHandlingService(ISnackbar snackbar)
    {
        _snackbar = snackbar;
    }

    public async Task HandleErrorAsync(HttpResponseMessage response)
    {
        var problemDetails = await response.Content.ReadFromJsonAsync<ApiResponseErrorDetails>();

        if (problemDetails.Errors.Any())
        {
            foreach (var error in problemDetails.Errors)
            {
                _snackbar.Add(error, Severity.Error);
            }

            return;
        }

        string errorMessage = problemDetails.Detail;
        _snackbar.Add(errorMessage, Severity.Error);
    }
}