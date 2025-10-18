using CandidateManagementSystem.Domain.Abstractions;
using MediatR;

namespace CandidateManagementSystem.Application.Abstractions.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>
{
}