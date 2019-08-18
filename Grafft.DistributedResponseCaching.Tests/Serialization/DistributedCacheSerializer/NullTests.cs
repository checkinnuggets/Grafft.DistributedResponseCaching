using Grafft.DistributedResponseCaching.Serialization;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.DistributedCacheSerializer
{
    public abstract class NullTests<TSerializer> : DistributedCacheSerializerTests<TSerializer>
        where TSerializer : IDistributedCacheSerializer, new()
    {
        [Fact]
        public void RoundtripNull()
        {
            // deserializes json representation of 'null'
            var result = Roundtrip(null);
            Assert.Null(result);
        }

        [Fact]
        public void DeserializeNull()
        {
            // deserializes .NET null
            var result = Serializer.Deserialize(null);
            Assert.Null(result);
        }
    }
}
