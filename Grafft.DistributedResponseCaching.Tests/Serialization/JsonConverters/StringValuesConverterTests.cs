using Grafft.DistributedResponseCaching.Serialization.JsonConverters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.JsonConverters
{
    public class ModelContainingAStringValues
    {
        public StringValues StringValues { get; set; }
    }

    public class StringValuesConverterTests
    {
        private readonly JsonConverter[] _converters = {
            new StringValuesConverter()
        };

        [Fact]
        public void RoundtripSingleValueItem()
        {
            var input = new ModelContainingAStringValues
            {
                StringValues = new StringValues("This is a test.")
            };

            var result = Roundtrip(input);

            Assert.Equal(input.StringValues, result.StringValues);
        }

        [Fact]
        public void RoundtripMultiValueItem()
        {
            var input = new ModelContainingAStringValues
            {
                StringValues = new StringValues(new[] { "This is a test.", "It has multiple values." })
            };

            var result = Roundtrip(input);

            Assert.Equal(input.StringValues, result.StringValues);
        }

        [Fact]
        public void RoundtripNull()
        {
            var input = new ModelContainingAStringValues
            {
                StringValues = new StringValues((string)null)
            };

            var result = Roundtrip(input);

            Assert.Equal(input.StringValues, result.StringValues);
        }

        [Fact]
        public void RoundtripEmptyString()
        {
            var input = new ModelContainingAStringValues
            {
                StringValues = new StringValues(string.Empty)
            };

            var result = Roundtrip(input);

            Assert.Equal(input.StringValues, result.StringValues);
        }


        private ModelContainingAStringValues Roundtrip(ModelContainingAStringValues origModel)
        {
            // Serialize...
            var json = JsonConvert.SerializeObject(origModel, _converters);

            // Deserialize...
            var result = JsonConvert.DeserializeObject<ModelContainingAStringValues>(json, _converters);

            return result;
        }
    }
}
