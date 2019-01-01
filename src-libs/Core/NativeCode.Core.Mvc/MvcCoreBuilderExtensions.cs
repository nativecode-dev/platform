namespace NativeCode.Core.Mvc
{
    using Microsoft.Extensions.DependencyInjection;

    public static class MvcCoreBuilderExtensions
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
