namespace NativeCode.Core.Data
{
    using System.Collections.Generic;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    ///     Context extension for additional properties.
    /// </summary>
    /// <remarks>
    ///     Most likely will need refactoring into a DataContextOptions type.
    /// </remarks>
    public class DataContextOptionsExtension : IDbContextOptionsExtensionWithDebugInfo
    {
        private long? serviceProviderHash;

        public bool EnableAuditing { get; set; } = true;

        public bool EnableValidation { get; set; } = true;

        public string LogFragment => GetFragment();

        /// <inheritdoc />
        public bool ApplyServices(IServiceCollection services)
        {
            return true;
        }

        /// <inheritdoc />
        public long GetServiceProviderHashCode()
        {
            if (this.serviceProviderHash.HasValue)
            {
                return this.serviceProviderHash.Value;
            }

            var auditing = this.EnableAuditing.GetHashCode();
            var validation = this.EnableValidation.GetHashCode();

            this.serviceProviderHash = auditing ^ validation;

            return this.serviceProviderHash.Value;
        }

        /// <inheritdoc />
        public void PopulateDebugInfo(IDictionary<string, string> debugInfo)
        {
            debugInfo.Add($"{nameof(DbContext)}:{nameof(this.EnableAuditing)}", this.EnableAuditing.ToString(CultureInfo.CurrentCulture));
            debugInfo.Add($"{nameof(DbContext)}:{nameof(this.EnableValidation)}", this.EnableAuditing.ToString(CultureInfo.CurrentCulture));
        }

        /// <inheritdoc />
        public void Validate(IDbContextOptions options)
        {
        }

        protected static string GetFragment()
        {
            return string.Empty;
        }
    }
}
