namespace NativeCode.Core.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using JetBrains.Annotations;

    using Microsoft.EntityFrameworkCore;

    using Newtonsoft.Json;

    public static class SeedDataExtensions
    {
        /// <summary>
        /// Loads seed data from a JSON file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        /// <exception cref="FileNotFoundException"></exception>
        public static ModelBuilder SeedJsonData<T>([NotNull] this ModelBuilder builder, [NotNull] string filename)
            where T : class
        {
            if (File.Exists(filename) == false)
            {
                throw new FileNotFoundException("Seed data JSON file does not exist", filename);
            }

            using (var reader = File.OpenText(filename))
            {
                var json = reader.ReadToEnd();
                var entities = JsonConvert.DeserializeObject<T[]>(json);

                return Seed(builder, entities);
            }
        }

        /// <summary>
        /// Loads seed data from an assembly manifest file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="manifest"></param>
        /// <returns></returns>
        public static ModelBuilder SeedJsonDataFromManifest<T>([NotNull] this ModelBuilder builder, [NotNull] string manifest)
            where T : class
        {
            var assembly = typeof(T).Assembly;
            var stream = assembly.GetManifestResourceStream(manifest);

            if (stream == null)
            {
                throw new FileNotFoundException("Unable to find manifest", manifest);
            }

            using (stream)
            {
                return builder.SeedJsonDataFromStream<T>(stream);
            }
        }

        /// <summary>
        /// Loads seed data from an assembly manifest file.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static ModelBuilder SeedJsonDataFromManifest<T>([NotNull] this ModelBuilder builder, [NotNull] Func<Type, string> callback)
            where T : class
        {
            return builder.SeedJsonDataFromManifest<T>(callback(typeof(T)));
        }

        /// <summary>
        /// Loads seed data from a stream instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="builder"></param>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static ModelBuilder SeedJsonDataFromStream<T>([NotNull] this ModelBuilder builder, Stream stream)
            where T : class
        {
            using (var reader = new StreamReader(stream))
            using (var jsonreader = new JsonTextReader(reader))
            {
                var serializer = new JsonSerializer();
                var entities = serializer.Deserialize<T[]>(jsonreader);

                return Seed(builder, entities);
            }
        }

        private static ModelBuilder Seed<T>(ModelBuilder builder, IEnumerable<T> entities)
            where T : class
        {
            foreach (var entity in entities)
            {
                builder.Entity<T>()
                    .HasData(entity);
            }

            return builder;
        }
    }
}
