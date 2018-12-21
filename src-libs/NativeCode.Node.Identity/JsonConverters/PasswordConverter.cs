namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using IdentityServer4.Models;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;

    public class PasswordConverter : JsonConverter
    {
        public PasswordConverter(IConfiguration configuration)
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
            if (reader.Value is string value && string.IsNullOrWhiteSpace(value) == false)
            {
                var bracketStarts = value.StartsWith("[");
                var bracketEnds = value.EndsWith("]");

                if (bracketStarts && bracketEnds)
                {
                    var section = value.Substring(1, value.Length - 2);
                    var password = this.Configuration.GetValue<string>(section);

                    if (string.IsNullOrWhiteSpace(password) == false)
                    {
                        return password.Sha512();
                    }
                }

                return value;
            }

            return reader.Value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
