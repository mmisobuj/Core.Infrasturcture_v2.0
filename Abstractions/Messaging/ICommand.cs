
using MediatR;

namespace Core.Infrastructure.Abstractions.Messaging
{


    public interface ICommand<out TResponse> : IRequest<TResponse>
    {
    }
}