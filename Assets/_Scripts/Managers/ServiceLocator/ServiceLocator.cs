using System;
using System.Collections.Generic;

namespace _Scripts.Managers.ServiceLocator
{
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> Services = new();

        public static void Register<T>(T service) where T : class
        {
            var type = typeof(T);
            Services[type] = service;
        }

        public static T Get<T>() where T : class
        {
            var type = typeof(T);

            if (Services.TryGetValue(type, out var service))
            {
                return service as T;
            }

            throw new Exception($"[ServiceLocator] Service of type {type} is not registered!");
        }
        
        public static bool TryGet<T>(out T service) where T : class
        {
            var type = typeof(T);

            if (Services.TryGetValue(type, out var obj))
            {
                service = obj as T;
                return true;
            }

            service = null;
            return false;
        }

        public static void Unregister<T>() where T : class
        {
            var type = typeof(T);
            Services.Remove(type);
        }

        public static void Clear() => Services.Clear();
    }
}