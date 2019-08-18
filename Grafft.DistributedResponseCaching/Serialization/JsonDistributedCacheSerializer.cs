using System.Text;
using Grafft.DistributedResponseCaching.Serialization.JsonConverters;
using Grafft.DistributedResponseCaching.Serialization.Models;
using Grafft.DistributedResponseCaching.Serialization.Types;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Newtonsoft.Json;

namespace Grafft.DistributedResponseCaching.Serialization
{
    public class JsonDistributedCacheSerializer : IDistributedCacheSerializer
    {
        private readonly Encoding _encoding = Encoding.UTF8;

        private readonly JsonConverter[] _converters =
        {
            new StreamConverter(),
            new HeaderDictionaryConverter(),
            new StringValuesConverter()
        };

        private readonly ITypeProvider _typeProvider;

        public JsonDistributedCacheSerializer()
        {
            _typeProvider = new TypeProvider(typeof(CachedResponse), typeof(CachedVaryByRules));
        }

        public byte[] Serialize(IResponseCacheEntry obj)
        {
            if (obj == null)
            {
                return null;
            }

            var entry = new CacheEntryWrapper
            {
                EntryType = _typeProvider.GetName(obj.GetType()),
                JsonContent = JsonConvert.SerializeObject(obj, _converters)
            };

            var json = JsonConvert.SerializeObject(entry);
            var serializedValue = _encoding.GetBytes(json);

            return serializedValue;
        }

        public IResponseCacheEntry Deserialize(byte[] buffer)
        {
            if (buffer == null)
            {
                return null;
            }

            var json = _encoding.GetString(buffer);
            var cacheEntry = JsonConvert.DeserializeObject<CacheEntryWrapper>(json, _converters);

            var type = _typeProvider.GetType(cacheEntry.EntryType);                
            var result = JsonConvert.DeserializeObject(cacheEntry.JsonContent, type, _converters);
                      
            return result as IResponseCacheEntry;
        }
    }
}