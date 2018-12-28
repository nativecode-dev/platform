namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using IdentityServer4.Models;
    using Newtonsoft.Json;

    public class ScopeConverter : JsonConverter
    {

        public override bool CanConvert(Type objectType)
        {
            return false;
        }
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string scope)
            {
                return new Scope(scope);
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
