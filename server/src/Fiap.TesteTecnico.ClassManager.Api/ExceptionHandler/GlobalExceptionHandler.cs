using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Fiap.TesteTecnico.ClassManager.Api.ExceptionHandler
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;

        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            _logger.LogError(exception, "An exception was thrown by the application: {Message}", exception.Message);

            httpContext.Response.ContentType = "application/json";

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = HttpStatusCode.InternalServerError.ToString(),
                Type = GetRfcLinkForStatusCode(StatusCodes.Status500InternalServerError),
                Detail = exception.Message
            };

            if (exception is CustomException customException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = HttpStatusCode.BadRequest.ToString();
                problemDetails.Type = GetRfcLinkForStatusCode(StatusCodes.Status400BadRequest);
                problemDetails.Detail = customException.Message;
            }

            if (exception is NotFoundException notFoundException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                problemDetails.Status = StatusCodes.Status404NotFound;
                problemDetails.Title = HttpStatusCode.NotFound.ToString();
                problemDetails.Type = GetRfcLinkForStatusCode(StatusCodes.Status404NotFound);
                problemDetails.Detail = notFoundException.Message;
            }

            if (exception is ValidationException validationException)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                problemDetails.Status = StatusCodes.Status400BadRequest;
                problemDetails.Title = HttpStatusCode.BadRequest.ToString();
                problemDetails.Type = GetRfcLinkForStatusCode(StatusCodes.Status400BadRequest);
                problemDetails.Detail = validationException.Message;
                problemDetails.Extensions["errors"] = validationException.Errors;
            }

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }

        private static string GetRfcLinkForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                StatusCodes.Status400BadRequest => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.1",
                StatusCodes.Status404NotFound => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.5.4",
                StatusCodes.Status500InternalServerError => "https://datatracker.ietf.org/doc/html/rfc7231#section-6.6.1",
                _ => "https://datatracker.ietf.org/doc/html/rfc7231"
            };
        }
    }
}
