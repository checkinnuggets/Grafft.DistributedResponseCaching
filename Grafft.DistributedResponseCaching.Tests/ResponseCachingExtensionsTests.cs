using System;
using Grafft.DistributedResponseCaching.Serialization;
using Grafft.DistributedResponseCaching.Tests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests
{
    public class ResponseCachingExtensionsTests
    {
        public class UseDistributedResponseCachingTests
        {
            [Fact]
            public void NullAppBuilderThrowsArgumentNullException()
            {
                IApplicationBuilder applicationBuilder = null;

                Action action = () => applicationBuilder.UseDistributedResponseCaching();

                Assert.Throws<ArgumentNullException>( action );
            }
        }

        public class AddDistributedResponseCachingTests
        {

            [Fact]
            public void NullServiceCollectionThrowsArgumentNullException()
            {
                IServiceCollection serviceCollection = null;

                Action action = () => serviceCollection.AddDistributedResponseCaching();

                Assert.Throws<ArgumentNullException>(action);
            }

            [Fact]
            public void ValidServiceCollectionWithoutOptionsHasConfiguredServices()
            {
                IServiceCollection serviceCollection = new ServiceCollection();

                serviceCollection.AddDistributedResponseCaching();

                EnsureDefaultImplementations(serviceCollection);

                EnsureOurImplementations(serviceCollection);
            }

            [Fact]
            public void ValidServiceCollectionWithoutOptionsHasConfiguredServicesAndOptions()
            {
                IServiceCollection serviceCollection = new ServiceCollection();


                // Call passing in parameter action
                serviceCollection.AddDistributedResponseCaching(options =>
                {
                    options.MaximumBodySize = 123;
                    options.SizeLimit = 456;
                    options.UseCaseSensitivePaths = true;
                });


                EnsureDefaultImplementations(serviceCollection);
                EnsureOurImplementations(serviceCollection);


                // Get configured options action...
                var configureOptions = serviceCollection.GetEntry<IConfigureOptions<ResponseCachingOptions>>();

                
                // Apply...
                var resultOptions = new ResponseCachingOptions();

                var typedOptions = configureOptions.ImplementationInstance as ConfigureNamedOptions<ResponseCachingOptions>;
                typedOptions.Action(resultOptions);

                // Check result...
                resultOptions.MaximumBodySize.Should().Be(123);
                resultOptions.SizeLimit.Should().Be(456);
                resultOptions.UseCaseSensitivePaths.Should().Be(true);
            }

            private static void EnsureDefaultImplementations(IServiceCollection serviceCollection)
            {
                // Default Implementations...
                serviceCollection.GetEntry<IResponseCachingPolicyProvider>()
                    .ImplementationType.Should().Be(typeof(ResponseCachingPolicyProvider));

                serviceCollection.GetEntry<IResponseCachingKeyProvider>()
                    .ImplementationType.Should().Be(typeof(ResponseCachingKeyProvider));
            }

            private static void EnsureOurImplementations(IServiceCollection serviceCollection)
            {
                // Our implementation...
                serviceCollection.GetEntry<IResponseCache>()
                    .ImplementationType.Should().Be(typeof(DistributedResponseCache));

                serviceCollection.GetEntry<IDistributedCacheSerializer>()
                    .ImplementationType.Should().Be(typeof(JsonDistributedCacheSerializer));
            }
        }
    }
}
