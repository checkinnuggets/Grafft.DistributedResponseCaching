using System;
using Grafft.DistributedResponseCaching.Serialization;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.DistributedResponseCacheTests
{
    public class ValidCacheEntryTypeTests
    {
        public class UnsupportedResponseCacheEntry : IResponseCacheEntry { }

        private const string Key = "Key";
        
        private readonly IDistributedCache _fakeDistributedCache;

        private readonly IResponseCache _distributedResponseCache;


        public ValidCacheEntryTypeTests()
        {
            // Setup DistributedResponseCache
            _distributedResponseCache = new DistributedResponseCache(_fakeDistributedCache, new JsonDistributedCacheSerializer());
        }

        [Fact]
        public void SetInvalidCacheEntryTypeThrowsException()
        {
            var unsupportedCacheEntry = new UnsupportedResponseCacheEntry();
            Assert.Throws<InvalidOperationException>(() =>
                _distributedResponseCache.Set(Key, unsupportedCacheEntry, default(TimeSpan)));
        }

        [Fact]
        public async void SetASyncInvalidCacheEntryTypeThrowsException()
        {
            var unsupportedCacheEntry = new UnsupportedResponseCacheEntry();
            await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _distributedResponseCache.SetAsync(Key, unsupportedCacheEntry, default(TimeSpan)));
        }
    }
}