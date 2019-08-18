using System.Collections.Generic;
using System.IO;
using Grafft.DistributedResponseCaching.Serialization.JsonConverters;
using Grafft.DistributedResponseCaching.Tests.Helpers;
using FluentAssertions;
using Newtonsoft.Json;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.JsonConverters
{
    public class ModelContainingAStream
    {
        public Stream Stream { get; set; }
    }

    public class StreamConverterTests
    {

        private readonly JsonSerializerSettings _settings
            = new JsonSerializerSettings
            {
                Converters = new List<JsonConverter> {new StreamConverter()}
            };


        [Theory]
        [InlineData(new byte[] {5, 4, 3, 2, 1})]
        [InlineData(new byte[0])]
        [InlineData(null)]
        public void RoundtripContent(byte[] origStreamContent)
        {

            // Setup input...
            var orig = new ModelContainingAStream
            {
                Stream = origStreamContent == null
                    ? null
                    : new MemoryStream(origStreamContent)
            };

            // Serialize...
            var json = JsonConvert.SerializeObject(orig, _settings);

            // Deserialize...
            var result = JsonConvert.DeserializeObject<ModelContainingAStream>(json, _settings);

            // Check...
            var resultStreamContent = StreamExtensions.CopyToArray(result.Stream);
            Assert.Equal(origStreamContent, resultStreamContent);
        }

        [Fact]
        public void EnsureInputStreamIsNotDisposed()
        {
            var orig = new ModelContainingAStream
            {
                Stream = new MemoryStream(new byte[] {1, 2, 3, 4, 5})
            };

            JsonConvert.SerializeObject(orig, _settings);


            orig.Stream.CanRead.Should().BeTrue();
            orig.Stream.CanWrite.Should().BeTrue();
        }

    }
}
