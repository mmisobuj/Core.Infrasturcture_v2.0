namespace Core.Infrastructure.Abstractions.Messaging
{


    public interface ICachedQuery<out TResponse> : IQuery<TResponse>
    {
        string CacheKey { get; }

        TimeSpan CacheFor { get; }
    }
}