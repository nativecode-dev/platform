namespace NativeCode.Core.Extensions
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Reflection;
    using Exceptions;
    using JetBrains.Annotations;

    public static class TypeExtensions
    {
        public static string AsKey(this Type[] types, string separator = ":")
        {
            return string.Join(separator, types.Select(type => type.Name));
        }

        public static Type GetClosedType(this Type type)
        {
            return type.GetClosedTypes()
                .First();
        }

        public static Type[] GetClosedTypes(this Type type)
        {
            if (type.GenericTypeArguments.Any() == false)
            {
                throw new InvalidClosedType(type);
            }

            return type.GenericTypeArguments;
        }

        public static bool HasBaseClass([NotNull] this Type source, [NotNull] Type type)
        {
            var current = source.GetTypeInfo();

            while (current.BaseType != null)
            {
                if (current.BaseType == type)
                {
                    return true;
                }

                current = current.BaseType.GetTypeInfo();
            }

            return false;
        }

        public static bool HasBaseClass<T>([NotNull] this Type source)
            where T : class
        {
            return source.HasBaseClass(typeof(T));
        }

        public static bool HasInterface([NotNull] this Type source, [NotNull] Type type)
        {
            return source.GetTypeInfo()
                .ImplementedInterfaces.Any(x => x == type);
        }

        public static bool HasInterface<T>([NotNull] this Type source)
        {
            return source.HasInterface(typeof(T));
        }

        public static bool IsCollection([NotNull] this Type type)
        {
            return type.HasInterface(typeof(ICollection));
        }

        public static bool IsEnumerable([NotNull] this Type type)
        {
            return type.HasInterface(typeof(IEnumerable));
        }

        public static bool IsList<T>([NotNull] this Type type)
        {
            return type.HasInterface(typeof(IList));
        }

        public static bool IsPlsAssembly([NotNull] this Assembly assembly)
        {
            return assembly.FullName.StartsWith("PLSos2");
        }

        public static bool IsPlsType([NotNull] this Type type)
        {
            return type.Namespace?.StartsWith("PLSos2") ?? false;
        }

        public static bool IsPlxAssembly([NotNull] this Assembly assembly)
        {
            return assembly.FullName?.StartsWith("PropLogix") ?? false;
        }

        public static bool IsPlxType([NotNull] this Type type)
        {
            return type.Namespace?.StartsWith("PropLogix") ?? false;
        }

        public static string ToFullKey([NotNull] this Type source)
        {
            return source.ToKey(x => x.AssemblyQualifiedName);
        }

        public static string ToKey([NotNull] this Type source)
        {
            return source.ToKey(x => x.FullName);
        }

        public static string ToKey([NotNull] this Type source, [NotNull] Func<Type, string> selector)
        {
            return selector(source);
        }

        public static string ToShortKey([NotNull] this Type source)
        {
            return source.ToKey(x => x.Name);
        }
    }
}