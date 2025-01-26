using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceBackend.Application.Abstractions.Services
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string?> GetCachedResponseAsync(string cacheKey);
        Task RemoveCacheByPattern(string pattern);
    }
}