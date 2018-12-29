namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class SecretConverter : JsonConverter
    {
        public SecretConverter(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected IConfiguration Configuration { get; }

        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value is string secret)
            {
                var bracketStarts = secret.StartsWith("[");
                var bracketEnds = secret.EndsWith("]");

                if (bracketStarts && bracketEnds)
                {
                    var section = secret.Substring(1, secret.Length - 2);
                    var password = this.Configuration.GetValue<string>(section);
                    return new Secret(password.Sha512());
                }

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
