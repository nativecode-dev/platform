namespace NativeCode.Node.Identity.JsonConverters
{
    using System.Reflection;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class IdentityContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            switch (property.PropertyName)
            {
                case "ApiSecrets":
                    property.ItemConverter = new SecretConverter();
                    return property;

                case "Claims":
                    property.ItemConverter = new ClaimConverter();
                    return property;

                case "ClientSecrets":
                    property.ItemConverter = new SecretConverter();
                    return property;

                case "Secrets":
                    property.ItemConverter = new SecretConverter();
                    return property;

                case "Scopes":
                    property.ItemConverter = new ScopeConverter();
                    return property;

                default:
                    return property;
            }
        }
    }
}
