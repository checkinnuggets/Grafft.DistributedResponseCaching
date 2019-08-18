using System.Threading;
using System.Threading.Tasks;
using Grafft.DistributedResponseCaching.Serialization;
using FakeItEasy;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.DistributedResponseCacheTests
{
    public class NullCacheEntryTests
    {
        private const string Key = "Key";

        private readonly IDistributedCache _fakeDistributedCache;
        private readonly IDistributedCacheSerializer _fakeCacheSerializer;

        private readonly IResponseCache _distributedResponseCache;

        public NullCacheEntryTests()
        {
            // Setup Distributed Cache
            _fakeDistributedCache = A.Fake<IDistributedCache>();

            A.CallTo(() => _fakeDistributedCache.Get(Key)).Returns(null);
            A.CallTo(() => _fakeDistributedCache.GetAsync(Key, default(CancellationToken))).Returns(Task.FromResult<byte[]>(null));

            // Setup Serializer
            _fakeCacheSerializer = A.Fake<IDistributedCacheSerializer>();

            // Setup DistributedResponseCache
            _distributedResponseCache = new DistributedResponseCache(_fakeDistributedCache, _fakeCacheSerializer);
        }

        [Fact]
        public void CacheGetNullReturnsNull()
        {

            var result = _distributedResponseCache.Get(Key);
            Assert.Null(result);
        }

        [Fact]
        public async void CacheGetAsyncNullReturnsNull()
        {
            var result = await _distributedResponseCache.GetAsync(Key);
            Assert.Null(result);
        }
    }
}