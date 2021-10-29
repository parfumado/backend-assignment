using System;
using System.Collections.Generic;

namespace CommonServices {
    public static class Injector {
        private static Dictionary<object, object[]> _mappings;

        static Injector() {
            _mappings = new Dictionary<object, object[]>();
        }

        public static void Map<T, Y>(params object[] parameters) where Y : notnull, T
        {
            _mappings.Add(typeof(T), parameters);
        }

        public static T GetInstance<T>() {
            T? instance = (T?)Activator.CreateInstance(typeof(T), _mappings[typeof(T)]);

            if (instance is null) {
                throw new InvalidOperationException($"There is no mapped implementation for `{typeof(T)}`");
            }

            return instance;
        }
    }
}