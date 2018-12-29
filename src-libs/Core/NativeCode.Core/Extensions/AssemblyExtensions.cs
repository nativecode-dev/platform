namespace NativeCode.Core.Extensions
{
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using Newtonsoft.Json;

    public static class AssemblyExtensions
    {
        public static string GetAssemblyFileVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();

            if (attribute != null)
            {
                return attribute.Version;
            }

            return string.Empty;
        }

        public static string GetAssemblyInformationalVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();

            if (attribute != null)
            {
                return attribute.InformationalVersion;
            }

            return string.Empty;
        }

        public static string GetAssemblyVersion(this Assembly assembly)
        {
            var attribute = assembly.GetCustomAttribute<AssemblyVersionAttribute>();

            if (attribute != null)
            {
                return attribute.Version;
            }

            return string.Empty;
        }

        public static string GetVersion(this Assembly assembly)
        {
            var versions = new[]
            {
                assembly.GetAssemblyFileVersion(), assembly.GetAssemblyInformationalVersion(),
                assembly.GetAssemblyVersion(),
            };

            return versions.FirstOrDefault(version => string.IsNullOrWhiteSpace(version) == false);
        }

        public static T JsonFromManifest<T>(this Assembly assembly, string name, JsonSerializerSettings settings)
        {
            var stream = assembly.GetManifestResourceStream(name);

            if (stream == null)
            {
                throw new FileNotFoundException("Unable to find manifest", name);
            }

            using (var reader = new StreamReader(stream))
            using (var jsonreader = new JsonTextReader(reader))
            {
                var serializer = JsonSerializer.Create(settings);
                return serializer.Deserialize<T>(jsonreader);
            }
        }

        public static T JsonFromManifestString<T>(this Assembly assembly, string name, JsonSerializerSettings settings)
        {
            var stream = assembly.GetManifestResourceStream(name);

            if (stream == null)
            {
                throw new FileNotFoundException("Unable to find manifest", name);
            }

            using (var reader = new StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                return JsonConvert.DeserializeObject<T>(json, settings);
            }
        }
    }
}
