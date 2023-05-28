using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Logic
{
    public static class ReflectionHelper
    {
        public static List<System.Type> FindDerivedTypes<T>(Assembly assembly)
        {
            var baseType = typeof(T);
            return assembly.GetTypes().Where(type => type != baseType && baseType.IsAssignableFrom(type)).ToList();
        }
    }
}