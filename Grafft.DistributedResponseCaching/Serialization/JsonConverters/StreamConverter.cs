using System;
using System.IO;
using Newtonsoft.Json;

namespace Grafft.DistributedResponseCaching.Serialization.JsonConverters
{
    public class StreamConverter : JsonConverter<Stream>
    {
        public override Stream ReadJson(JsonReader reader, Type objectType, Stream existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return null;
            }

            var base64Val = (string)reader.Value;
            var buffer = Convert.FromBase64String(base64Val);
            return new MemoryStream(buffer);
        }

        public override void WriteJson(JsonWriter writer, Stream value, JsonSerializer serializer)
        {
            using (var inputStream = new MemoryStream())
            {
                // take a copy of 'value' so we don't dispose of the input stream.
                 value.CopyTo(inputStream);
                 inputStream.Position = 0;

                using (var reader = new BinaryReader(inputStream))
                {
                    var buffer = reader.ReadBytes((int)inputStream.Length);
                    var base64Val = Convert.ToBase64String(buffer);
                    writer.WriteValue(base64Val);
                }
            }
        }
    }
}
