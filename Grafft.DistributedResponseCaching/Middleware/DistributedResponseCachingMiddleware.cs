using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Grafft.DistributedResponseCaching.Middleware
{
    public class DistributedResponseCachingMiddleware : ResponseCachingMiddleware
    {
        private const string InternalCacheFieldName = "_cache";

        public DistributedResponseCachingMiddleware(
            RequestDelegate next,
            IOptions<ResponseCachingOptions> options,
            ILoggerFactory loggerFactory,
            IResponseCachingPolicyProvider policyProvider,
            IResponseCache cache,
            IResponseCachingKeyProvider keyProvider)
            : base(next, options, loggerFactory, policyProvider, keyProvider)
        {
            var cacheFieldInfo = typeof(ResponseCachingMiddleware)
                .GetField(InternalCacheFieldName, BindingFlags.NonPublic | BindingFlags.Instance);

            if (cacheFieldInfo == null)
            {
                throw new Exception($"Failed to internal cache field named '{InternalCacheFieldName}'.");
            }

            cacheFieldInfo.SetValue(this, cache);
        }
    }
}
