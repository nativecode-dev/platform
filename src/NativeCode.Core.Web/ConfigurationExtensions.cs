namespace NativeCode.Core.Web
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ConfigurationExtensions
    {
        public static IMvcCoreBuilder AddModelValidator(this IMvcCoreBuilder builder)
        {
            return builder.AddMvcOptions(options => options.Filters.Add(new ModelValidatorFilterAttribute()));
        }

        public static IMvcBuilder AddModelValidator(this IMvcBuilder builder)
        {
            return builder.AddMvcOptions(options => options.Filters.Add(new ModelValidatorFilterAttribute()));
        }
    }
}
