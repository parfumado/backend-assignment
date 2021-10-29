using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Utility.Runtime {
    public static class TypeUtils {
        public static bool IsAnonymous(this Type type) {
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                    && type.IsGenericType
                    && type.Name.Contains("AnonymousType")
                    && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase) ||
                        type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase))
                    && (type.Attributes & TypeAttributes.NotPublic) == TypeAttributes.NotPublic;
        }

        public static bool IsPrimitiveOrString(this Type type) {
            return type.IsPrimitive || type.Equals(typeof(string));
        }
    }
}