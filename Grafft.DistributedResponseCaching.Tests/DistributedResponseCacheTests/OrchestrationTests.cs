using System;
using System.Threading;
using Grafft.DistributedResponseCaching.Serialization;
using FakeItEasy;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.DistributedResponseCacheTests
{
    public class OrchestrationTests
    {
        private const string Key = "Key";
        private readonly byte[] _serializedValue = { 5, 4, 3, 2, 1 };
        private readonly CachedResponse _deserializedValue = new CachedResponse();


        private readonly IDistributedCache _fakeDistributedCache;
        private readonly IDistributedCacheSerializer _fakeCacheSerializer;

        private readonly IResponseCache _distributedResponseCache;

        public OrchestrationTests()
        {
            // Setup Distributed Cache
            _fakeDistributedCache = A.Fake<IDistributedCache>();

            A.CallTo(() => _fakeDistributedCache.Get(Key)).Returns(_serializedValue);
            A.CallTo(() => _fakeDistributedCache.GetAsync(Key, default(CancellationToken))).Returns(_serializedValue);

            // Setup Serializer
            _fakeCacheSerializer = A.Fake<IDistributedCacheSerializer>();
            A.CallTo(() => _fakeCacheSerializer.Serialize(_deserializedValue)).Returns(_serializedValue);
            A.CallTo(() => _fakeCacheSerializer.Deserialize(_serializedValue)).Returns(_deserializedValue);

            // Setup DistributedResponseCache
            _distributedResponseCache = new DistributedResponseCache(_fakeDistributedCache, _fakeCacheSerializer);
        }

        [Fact]
        public void GetCallsCacheAndSerializer()
        {
            _distributedResponseCache.Get(Key);

            A.CallTo(() => _fakeDistributedCache.Get(Key))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeCacheSerializer.Deserialize(_serializedValue))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async void GetAsyncCallsCacheAndSerializer()
        {
            await _distributedResponseCache.GetAsync(Key);


            A.CallTo(() => _fakeDistributedCache.GetAsync(Key, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeCacheSerializer.Deserialize(_serializedValue))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void SetCallsSerializerAndCache()
        {
            _distributedResponseCache.Set(Key, _deserializedValue, default(TimeSpan));

            A.CallTo(() => _fakeCacheSerializer.Serialize(_deserializedValue))
               .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeDistributedCache.Set(Key, _serializedValue, A<DistributedCacheEntryOptions>._))
                .MustHaveHappenedOnceExactly();
        }

        [Fact]
        public void SetAsyncCallsSerializerAndCache()
        {
            _distributedResponseCache.SetAsync(Key, _deserializedValue, default(TimeSpan));

            A.CallTo(() => _fakeCacheSerializer.Serialize(_deserializedValue))
                .MustHaveHappenedOnceExactly();

            A.CallTo(() => _fakeDistributedCache.SetAsync(Key, _serializedValue, A<DistributedCacheEntryOptions>._, A<CancellationToken>._))
                .MustHaveHappenedOnceExactly();
        }
    }
}