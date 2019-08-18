using System;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;

namespace Grafft.DistributedResponseCaching.Serialization.JsonConverters
{
    public class StringValuesConverter : JsonConverter<StringValues>
    {
        public override StringValues ReadJson(JsonReader reader, Type objectType, StringValues existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return default(StringValues);
            }

            var json = (string)reader.Value;
            var arr = JsonConvert.DeserializeObject<string[]>(json);

            return new StringValues(arr);
        }

        public override void WriteJson(JsonWriter writer, StringValues value, JsonSerializer serializer)
        {
            var arr = value.ToArray();
            var json = JsonConvert.SerializeObject(arr);
            writer.WriteValue(json);
        }
    }
}