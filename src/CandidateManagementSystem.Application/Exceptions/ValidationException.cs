using CandidateManagementSystem.Application.Abstractions.Behaviours;

namespace CandidateManagementSystem.Application.Exceptions;

public sealed class ValidationException : Exception
{
    public ValidationException(IEnumerable<ValidationError> errors)
    {
        Erros = errors;
    }
    
    public IEnumerable<ValidationError> Erros { get; }
}