using CandidateManagementSystem.Domain.Abstractions;
using MediatR;

namespace CandidateManagementSystem.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>
{
}