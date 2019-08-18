using System.Collections.Generic;
using Grafft.DistributedResponseCaching.Serialization.JsonConverters;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Serialization.JsonConverters
{
    public class ModelContainingAHeaderDictionary
    {
        public IHeaderDictionary HeaderDictionary { get; set; }
    }

    public class HeaderDictionaryConverterTests
    {
        private readonly JsonConverter[] _converters = {
            new HeaderDictionaryConverter(),
        };

        [Fact]
        public void RoundtripValidContent()
        {
            var origHeaderDictionary = new HeaderDictionary(new Dictionary<string, StringValues>
            {
                {"Key A", new StringValues("A Single Value")},
                {"Key B", new StringValues()}, // empty value
                {"Key C", new StringValues(new []{"A", "Collection", "Of", "Values"})},
                {string.Empty, new StringValues("An empty key")}
            });


            // Setup input...
            var origModel = new ModelContainingAHeaderDictionary
            {
                HeaderDictionary = origHeaderDictionary
            };

            // 
            var result = Roundtrip(origModel);

            // Check...
            Assert.Equal(origHeaderDictionary, result.HeaderDictionary);
        }

        [Fact]
        public void RoundtripNull()
        {
            // Setup input...
            var origModel = new ModelContainingAHeaderDictionary
            {
                HeaderDictionary = null
            };

            // 
            var result = Roundtrip(origModel);

            // Check...
            Assert.Null(result.HeaderDictionary);
        }


        // Can't use '[InlineData]' with complex types so...
        private ModelContainingAHeaderDictionary Roundtrip(ModelContainingAHeaderDictionary origModel)
        {
            // Serialize...
            var json = JsonConvert.SerializeObject(origModel, _converters);

            // Deserialize...
            var result = JsonConvert.DeserializeObject<ModelContainingAHeaderDictionary>(json, _converters);

            return result;
        }
    }
}