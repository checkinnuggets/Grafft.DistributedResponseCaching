using Grafft.DistributedResponseCaching.Serialization;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.DistributedCacheSerializer
{
    public abstract class CachedVaryByRules<TSerializer> : DistributedCacheSerializerTests<TSerializer>
        where TSerializer : IDistributedCacheSerializer, new()
    {
        private readonly CachedVaryByRules _exampleCachedVaryByRules = new CachedVaryByRules
        {
            Headers = new StringValues(new []{ "a", "b", "c"}),
            QueryKeys = new StringValues(new[] { "a", "b", "c" }),
            VaryByKeyPrefix = "qwerty"
        };

        [Fact]
        public void RoundTripWithAllProperties()
        {
            var original = _exampleCachedVaryByRules;

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }


        [Fact]
        public void RoundTripWithSingleValueHeaders()
        {
            var original = _exampleCachedVaryByRules;
            original.Headers = new StringValues("a single value");

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithMultiValueHeaders()
        {
            var original = _exampleCachedVaryByRules;
            original.Headers = new StringValues(new []{"multiple", "values"});

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithNullHeaders()
        {
            var original = _exampleCachedVaryByRules;
            original.Headers = new StringValues((string)null);

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithEmptyHeaders()
        {
            var original = _exampleCachedVaryByRules;
            original.Headers = new StringValues(string.Empty);

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithSingleValueQueryKeys()
        {
            var original = _exampleCachedVaryByRules;
            original.QueryKeys = new StringValues("single value");

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithMultiValueQueryKeys()
        {
            var original = _exampleCachedVaryByRules;
            original.QueryKeys = new StringValues(new []{"multiple", "values"});

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithNullQueryKeys()
        {
            var original = _exampleCachedVaryByRules;
            original.QueryKeys = new StringValues((string)null);

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithNullVaryByKeyPrefix()
        {
            var original = _exampleCachedVaryByRules;
            original.VaryByKeyPrefix = null;

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }

        [Fact]
        public void RoundTripWithEmptyVaryByKeyPrefix()
        {
            var original = _exampleCachedVaryByRules;
            original.VaryByKeyPrefix = string.Empty;

            var result = Roundtrip(original) as CachedVaryByRules;

            Assert.NotNull(result);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.QueryKeys, result.QueryKeys);
            Assert.Equal(original.VaryByKeyPrefix, result.VaryByKeyPrefix);
        }
    }
}
