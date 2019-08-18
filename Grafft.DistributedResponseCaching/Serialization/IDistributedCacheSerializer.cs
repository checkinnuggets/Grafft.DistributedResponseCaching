using Microsoft.AspNetCore.ResponseCaching.Internal;

namespace Grafft.DistributedResponseCaching.Serialization
{
    public interface IDistributedCacheSerializer
    {
        byte[] Serialize(IResponseCacheEntry obj);
        IResponseCacheEntry Deserialize(byte[] buffer);        
    }
}
