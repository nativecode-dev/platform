namespace NativeCode.Core
{
    using System;
    using System.Reflection;

    public static class Info
    {
        public static string AppVersion<T>()
        {
            return AppVersion(typeof(T));
        }

        public static string AppVersion(Type type)
        {
            return AppVersion(type.Assembly);
        }

        public static string AppVersion(Assembly assembly)
        {
            var aiva = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
            if (aiva != null)
            {
                return aiva.InformationalVersion;
            }

            var afva = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
            if (afva != null)
            {
                return afva.Version;
            }

            var ava = assembly.GetCustomAttribute<AssemblyVersionAttribute>();
            if (ava != null)
            {
                return ava.Version;
            }

            return string.Empty;
        }
    }
}