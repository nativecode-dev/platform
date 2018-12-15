namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using IdentityServer4.Models;
    using Newtonsoft.Json;

    public class SecretConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.Value is string secret)
            {
                return new Secret(secret.Sha512());
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
