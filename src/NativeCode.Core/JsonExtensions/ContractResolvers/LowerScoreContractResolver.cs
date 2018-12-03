namespace NativeCode.Core.JsonExtensions.ContractResolvers
{
    using System.Globalization;
    using Humanizer;
    using Newtonsoft.Json.Serialization;

    public class LowerScoreContractResolver : DefaultContractResolver
    {
        protected override string ResolvePropertyName(string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return base.ResolvePropertyName(propertyName);
            }

            return propertyName.Underscore()
                .ToLower(CultureInfo.CurrentCulture);
        }
    }
}