using MediatR;

namespace Core.Infrastructure.Abstractions.Messaging
{

    public interface IQuery<out TResponse> : IRequest<TResponse>
    {
    }
}