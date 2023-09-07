using MediatR;

namespace Core.Infrastructure.Abstractions.Messaging
{
 

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}
}