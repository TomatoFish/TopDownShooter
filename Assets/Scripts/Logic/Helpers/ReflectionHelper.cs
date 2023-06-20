using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Zenject;

namespace Logic
{
    public static class ReflectionHelper
    {
        public static List<Type> FindDerivedTypes<T>(Assembly assembly)
        {
            var baseType = typeof(T);
            return assembly.GetTypes().Where(type => type != baseType && baseType.IsAssignableFrom(type)).ToList();
        }

        public static void FindDerivedTypes<T>(Assembly assembly, DiContainer container, Action<Type, T> action, object[] args = null)
        {
            var types = FindDerivedTypes<T>(assembly);
            foreach (var type in types)
            {
                var widget = (T)(args == null ? container.Instantiate(type) : container.Instantiate(type, args));
                action.Invoke(type, widget);
            }
        }
    }
}