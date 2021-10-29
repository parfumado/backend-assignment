using System;
using System.Linq;
using System.Reflection;

namespace Utility.Runtime {
    public static class ObjectUtils {
        public static Y CopyProperties<T, Y>(T source, bool ignoreNull = true, Y? dest = default) {
            Y typedDest = dest ?? Activator.CreateInstance<Y>();

            foreach (PropertyInfo property in typeof(T).GetProperties().Where(p => p.CanRead)) {
                PropertyInfo? destProperty = typedDest!.GetType().GetProperty(property.Name);
                if (destProperty is null || !destProperty.CanWrite) continue;

                object? value = property.GetValue(source, null);

                if ((value is null && ignoreNull) || (value is not null && value.GetType().IsValueType && value == default) || (value is not null && !destProperty.PropertyType.IsAssignableFrom(value.GetType()))) {
                    continue;
                }

                destProperty.SetValue(typedDest, value, null);
            }

            return typedDest;
        }

    }
}