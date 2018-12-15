namespace NativeCode.Node.Identity.JsonConverters
{
    using System;
    using System.Collections.ObjectModel;
    using System.Security.Claims;
    using Newtonsoft.Json;

    public class ClaimConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return false;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            var value = reader.Value;

            if (value == null)
            {
                return new Collection<Claim>();
            }

            if (value is string claim)
            {
                var split = claim.Split(Convert.ToChar("::"));
                return new Claim(split[0], split[1]);
            }

            return value;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
