namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using System.Reflection;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class IdentityContractResolver : DefaultContractResolver
    {
        public IdentityContractResolver(IServiceProvider provider)
        {
            this.Provider = provider;
        }

        protected IServiceProvider Provider { get; }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            switch (property.PropertyName)
            {
                case "ApiSecrets":
                    property.ItemConverter = this.Provider.GetService<SecretConverter>();
                    return property;

                case "Claims":
                    property.ItemConverter = this.Provider.GetService<ClaimConverter>();
                    return property;

                case "ClientSecrets":
                    property.ItemConverter = this.Provider.GetService<SecretConverter>();
                    return property;

                case "Secrets":
                    property.ItemConverter = this.Provider.GetService<SecretConverter>();
                    return property;

                case "Scopes":
                    property.ItemConverter = this.Provider.GetService<ScopeConverter>();
                    return property;

                default:
                    return property;
            }
        }
    }
}
