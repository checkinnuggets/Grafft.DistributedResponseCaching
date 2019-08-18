using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Grafft.DistributedResponseCaching.Serialization.JsonConverters
{
    public class HeaderDictionaryConverter : JsonConverter<IHeaderDictionary>
    {
        private readonly JsonConverter[] _converters = {
            new StringValuesConverter()
        };       

        public override IHeaderDictionary ReadJson(JsonReader reader, Type objectType, IHeaderDictionary existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var json = (string)reader.Value;
            var dict = JsonConvert.DeserializeObject<Dictionary<string, StringValues>>(json, _converters);

            var stringValues = dict.ToDictionary(x => x.Key, x => x.Value);
            return new HeaderDictionary(stringValues);
        }

        public override void WriteJson(JsonWriter writer, IHeaderDictionary value, JsonSerializer serializer)
        {
            var dict = value.ToDictionary(x => x.Key, x => x.Value);
            var json = JsonConvert.SerializeObject(dict, _converters);
            writer.WriteValue(json);
        }
    }
}