using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Grafft.DistributedResponseCaching.Tests.Helpers
{
    public static class ServiceCollectionExtensions
    {
        public static ServiceDescriptor GetEntry<T>(this IServiceCollection services)
        {
            return services.SingleOrDefault(x => x.ServiceType == typeof(T));
        }
    }
}
