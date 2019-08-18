using System;
using System.Collections.Generic;
using System.IO;
using Grafft.DistributedResponseCaching.Serialization;
using Grafft.DistributedResponseCaching.Tests.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.DistributedCacheSerializer
{
    public abstract class CachedResponseTests<TSerializer> : DistributedCacheSerializerTests<TSerializer>
        where TSerializer : IDistributedCacheSerializer, new()
    {

        private readonly CachedResponse _exampleCachedResponse = new CachedResponse
        {
            StatusCode = 200,
            Created = DateTimeOffset.MaxValue,
            Headers = new HeaderDictionary(new Dictionary<string, StringValues>
            {
                {"abv", new StringValues("abx")}
            }),
            Body = new MemoryStream(new byte[] { 1, 2, 3, 4, 5 })
        };

        
        [Fact]        
        public void RoundtripWithAllProperties()
        {
            var original = _exampleCachedResponse;

            var result = Roundtrip(original) as CachedResponse;

            Assert.NotNull(result);
            Assert.Equal(original.StatusCode, result.StatusCode);
            Assert.Equal(original.Created, result.Created);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.Body.CopyToArray(), result.Body.CopyToArray());
        }

        [Fact]
        public void RoundtripWithNullHeaders()
        {
            var original = _exampleCachedResponse;
            original.Headers = null;

            var result = Roundtrip(original) as CachedResponse;

            Assert.NotNull(result);
            Assert.Equal(original.StatusCode, result.StatusCode);
            Assert.Equal(original.Created, result.Created);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.Body.CopyToArray(), result.Body.CopyToArray());
        }

        [Fact]
        public void RoundtripWithNullBody()
        {
            var original = _exampleCachedResponse;
            original.Body = null;

            var result = Roundtrip(original) as CachedResponse;

            Assert.NotNull(result);
            Assert.Equal(original.StatusCode, result.StatusCode);
            Assert.Equal(original.Created, result.Created);
            Assert.Equal(original.Headers, result.Headers);
            Assert.Equal(original.Body.CopyToArray(), result.Body.CopyToArray());
        }
    }
}
