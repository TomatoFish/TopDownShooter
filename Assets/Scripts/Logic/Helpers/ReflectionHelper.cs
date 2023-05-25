using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logic
{
    public static class ReflectionHelper
    {
        public static IEnumerable<System.Type> FindDerivedTypes<T>(Assembly assembly)
        {
            var baseType = typeof(T);
            return assembly.GetTypes().Where(type => type != baseType && type.IsAssignableFrom(baseType));
        }
    }
}