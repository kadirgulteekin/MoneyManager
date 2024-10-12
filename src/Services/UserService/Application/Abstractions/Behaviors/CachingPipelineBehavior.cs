using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Application.Abstractions.Behaviors;

public interface ICachableRequest
{
    //we can add cache settings as property here , cache key, expiration time vb..)
}
internal sealed class CachingPipelineBehavior<TRequest, TResponse> :
    IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ICachableRequest
{
    private readonly IMemoryCache _cache;
    private readonly ILogger<CachingPipelineBehavior<TRequest, TResponse>> _logger;

    public CachingPipelineBehavior(IMemoryCache cache, ILogger<CachingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;
        var cacheKey = $"{typeof(TRequest).Name}_{request.GetHashCode()}";
        if(_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
        {
            _logger.LogInformation("Processing request {RequestName}", requestName);
            return cachedResponse;
        }
        var response = await next();
        _cache.Set(cacheKey, response,TimeSpan.FromMinutes(5));
        return response;
    }
}
