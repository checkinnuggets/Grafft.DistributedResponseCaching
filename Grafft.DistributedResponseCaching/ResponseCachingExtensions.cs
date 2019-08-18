using System;
using Grafft.DistributedResponseCaching.Middleware;
using Grafft.DistributedResponseCaching.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCaching;
using Microsoft.AspNetCore.ResponseCaching.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace Grafft.DistributedResponseCaching
{
   /*
        Note that these methods are to be used instead of, and not in addition to, the default ResponseCaching.
    
        The middleware extends the default ResponseCachingMiddleware, and both can't be registered at the same time.
        The service configuration was then written to mirror this.
    */
    public static class ResponseCachingExtensions
    {
        public static IApplicationBuilder UseDistributedResponseCaching(this IApplicationBuilder app)
        {
            // The middleware can be re

            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<DistributedResponseCachingMiddleware>();
        }

        public static IServiceCollection AddDistributedResponseCaching(this IServiceCollection services, Action<ResponseCachingOptions> configureOptions = null)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configureOptions == null)
            {
                services.AddResponseCaching();
            }
            else
            {
                services.AddResponseCaching(configureOptions);
            }


            services.AddSingleton<IResponseCache, DistributedResponseCache>();
            services.AddTransient<IDistributedCacheSerializer, JsonDistributedCacheSerializer>();

            return services;
        }
    }
}