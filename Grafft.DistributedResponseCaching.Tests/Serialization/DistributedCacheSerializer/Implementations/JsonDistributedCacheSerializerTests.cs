using Grafft.DistributedResponseCaching.Serialization;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.DistributedCacheSerializer.Implementations
{
    public class JsonDistributedCacheSerializerTests
    {
        public class SerializerCachedResponseTests : CachedResponseTests<JsonDistributedCacheSerializer> { }
        public class SerializerCachedVaryByRulesTests : CachedVaryByRules<JsonDistributedCacheSerializer> { }
        public class SerializerNullTests : NullTests<JsonDistributedCacheSerializer> { }
    }   
}
