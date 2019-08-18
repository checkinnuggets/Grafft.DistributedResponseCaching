using Grafft.DistributedResponseCaching.Serialization;
using Microsoft.AspNetCore.ResponseCaching.Internal;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.DistributedCacheSerializer
{
    public abstract class DistributedCacheSerializerTests<TResponseCacheEntry>
        where TResponseCacheEntry : IDistributedCacheSerializer, new()
    {
        protected IDistributedCacheSerializer Serializer { get; }
            = new TResponseCacheEntry();

        protected IResponseCacheEntry Roundtrip(IResponseCacheEntry inputResponse)
        {
            var serializedValue = Serializer.Serialize(inputResponse);
            var deserializedValue = Serializer.Deserialize(serializedValue);

            return deserializedValue;
        }
    }
}