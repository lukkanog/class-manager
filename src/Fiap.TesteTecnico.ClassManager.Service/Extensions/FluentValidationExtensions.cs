using Fiap.TesteTecnico.ClassManager.Domain.Exceptions;
using FluentValidation.Results;

namespace Fiap.TesteTecnico.ClassManager.Service.Extensions;

public static class FluentValidationExtensions
{
    public static void ThrowExceptionIfNotValid(this ValidationResult validationResult, string message)
    {
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            throw new ValidationFailureException(message, errors);
        }
    }

    public static void ThrowExceptionIfNotValid(this ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToArray();
            throw new ValidationFailureException($"Validation failed", errors);
        }
    }
}
