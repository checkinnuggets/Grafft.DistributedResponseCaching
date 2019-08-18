using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Grafft.DistributedResponseCaching.Serialization;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Caching.Distributed;

[assembly: InternalsVisibleTo("Grafft.DistributedResponseCaching.Tests")]


namespace Grafft.DistributedResponseCaching
{
    public class DistributedResponseCache : IResponseCache
    {
        /*
         * Note that in implementing this interface, we only need to be concerned 
         * with the persistence.  We don't need to do anything like manage
         * the lifetime of the cache entries - this is done by the middleware itself.
         */

        private readonly IDistributedCache _distributedCache;
        private readonly IDistributedCacheSerializer _serializer;

        public DistributedResponseCache(IDistributedCache distributedCache, IDistributedCacheSerializer serializer)
        {
            _distributedCache = distributedCache;
            _serializer = serializer;            
        }

        public IResponseCacheEntry Get(string key)
        {
            var serializedValue = _distributedCache.Get(key);
            if (serializedValue == null)
            {
                return null;
            }

            var deserializedValue = _serializer.Deserialize(serializedValue);
            return deserializedValue;
        }

        public async Task<IResponseCacheEntry> GetAsync(string key)
        {
            var serializedValue = await _distributedCache.GetAsync(key);
            if (serializedValue == null)
            {
                return null;
            }

            var deserializedValue = _serializer.Deserialize(serializedValue);                 
            return deserializedValue;
        }

        public void Set(string key, IResponseCacheEntry entry, TimeSpan validFor)
        {
            var serializedValue = _serializer.Serialize(entry);
            _distributedCache.Set(key, serializedValue);
        }

        public async Task SetAsync(string key, IResponseCacheEntry entry, TimeSpan validFor)
        {
            var serializedValue = _serializer.Serialize(entry);
            await _distributedCache.SetAsync(key, serializedValue);
        }
    }
}
