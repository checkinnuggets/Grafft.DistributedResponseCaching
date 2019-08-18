using System.Reflection;
using Grafft.DistributedResponseCaching.Middleware;
using FakeItEasy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Xunit;

namespace Grafft.DistributedResponseCaching.Tests.Middleware
{
    public class DistributedResponseCachingMiddlewareTests
    {
        [Fact]
        public void MiddlewareUsesImplementationUsesSpecifiedResponseCache()
        {
            var suppliedCache = A.Fake<IResponseCache>();
            
            // Create middleware instance...
            var middleware = new DistributedResponseCachingMiddleware(
                A.Fake<RequestDelegate>(), 
                A.Fake<IOptions<ResponseCachingOptions>>(), 
                A.Fake<ILoggerFactory>(), 
                A.Fake<IResponseCachingPolicyProvider>(), 
                suppliedCache,
                A.Fake<IResponseCachingKeyProvider>());

            // Cache instance is private, so grab using reflection...
            var cacheFieldInfo = typeof(ResponseCachingMiddleware)
                .GetField("_cache", BindingFlags.NonPublic | BindingFlags.Instance);

            var usedCache = cacheFieldInfo.GetValue(middleware) as IResponseCache;

            // Verify...
            Assert.Equal(suppliedCache, usedCache);
        }
    }
}
