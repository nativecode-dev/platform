namespace NativeCode.Sync.SyncActions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    using Newtonsoft.Json;

    public abstract class SyncDescriptor : ISyncDescriptor
    {
        private readonly IDistributedCache cache;

        protected SyncDescriptor(IDistributedCache cache, string userAgent)
        {
            this.cache = cache;
            this.UserAgent = userAgent;
        }

        public string UserAgent { get; }

        public abstract Task CreateKey(string key, ISyncDescriptor source);

        public Task<IReadOnlyCollection<string>> GetKeys()
        {
            return this.Value(key => this.DescriptorKeys());
        }

        public async Task<object> GetKeyValue(string key, string name)
        {
            var cachekey = $"{key}:{name}";
            return await this.Value(cachekey, k => this.DescriptorValue(key, name));
        }

        public abstract Task RemoveKey(string key, ISyncDescriptor source);

        protected abstract Task<IReadOnlyCollection<string>> DescriptorKeys();

        protected abstract Task<object> DescriptorValue(string key, string name);

        protected async Task<T> GetCache<T>(string key, T defaultValue = default)
        {
            var cached = await this.cache.GetAsync(CreateKey<T>(key));

            if (cached != null && cached.Any())
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(cached));
            }

            return defaultValue;
        }

        protected object GetPropertyValue(string name, object instance)
        {
            var property = instance.GetType().GetProperties().Single(p => string.Equals(p.Name, name, StringComparison.CurrentCultureIgnoreCase));
            return property.GetValue(instance);
        }

        protected Task SetCache<T>(string key, T value)
        {
            var json = JsonConvert.SerializeObject(value);
            return this.cache.SetStringAsync(CreateKey<T>(key), json);
        }

        protected Task<T> Value<T>(Func<string, Task<T>> factory)
        {
            var key = typeof(T).Name;
            return this.Value(key, factory);
        }

        protected async Task<T> Value<T>(string key, Func<string, Task<T>> factory)
        {
            var cached = await this.GetCache<T>(key);

            if (cached == null)
            {
                var value = await factory(key);
                await this.SetCache(key, value);

                return value;
            }

            return cached;
        }

        private static string CreateKey<T>(string key)
        {
            return $"{Environment.MachineName}:{typeof(T).Name}:{key}";
        }
    }
}
