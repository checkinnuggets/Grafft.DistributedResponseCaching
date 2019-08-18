using System;

namespace Grafft.DistributedResponseCaching.Serialization.Types
{
    public interface ITypeProvider
    {
        string GetName(Type type);
        Type GetType(string typeName);
    }
}