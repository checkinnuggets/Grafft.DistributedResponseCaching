using System;
using System.Linq;

namespace Grafft.DistributedResponseCaching.Serialization.Types
{
    public class TypeProvider : ITypeProvider
    {
        private static string Key(Type type)
            => type.FullName;

        private readonly Type[] _supportedTypes;

        public TypeProvider(params Type[] supportedTypes)
        {
            _supportedTypes = supportedTypes;
        }
        
        public string GetName(Type type)
        {
            EnsureTypeIsValid(type);
            return Key(type);
        }

        public Type GetType(string typeName)
        {           
            var type = _supportedTypes.SingleOrDefault(t => Key(t) == typeName);
            EnsureTypeIsValid(type);
            return type;
        }
        
        private void EnsureTypeIsValid(Type type)
        {
            if (!_supportedTypes.Contains(type))
            {
                throw new InvalidOperationException($"Type '{type.FullName}' is not supported.");
            }
        }
    }
}