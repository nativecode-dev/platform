namespace NativeCode.Core.Data.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using Microsoft.Extensions.Logging;

    internal class SqlLoggingProvider : ILoggerProvider
    {
        private static readonly ConcurrentDictionary<Type, SqlLoggingProvider> Providers =
            new ConcurrentDictionary<Type, SqlLoggingProvider>();

        private volatile LoggingConfiguration configuration;

        private SqlLoggingProvider(Action<string> logger, Func<string, LogLevel, bool> filter)
        {
            this.configuration = new LoggingConfiguration(logger, filter);
        }

        public ILogger CreateLogger(string category)
        {
            return new Logger(category, this);
        }

        public void Dispose()
        {
        }

        public static void CreateOrModifyLoggerForDbContext(
            Type dbtype,
            ILoggerFactory factory,
            Action<string> logger,
            Func<string, LogLevel, bool> filter = null)
        {
            var created = false;

            SqlLoggingProvider ValueFactory(Type type)
            {
                var loggingProvider = new SqlLoggingProvider(logger, filter ?? DefaultFilter);
                factory.AddProvider(loggingProvider);
                created = true;
                return loggingProvider;
            }

            var provider = Providers.GetOrAdd(dbtype, ValueFactory);

            if (created == false)
            {
                provider.configuration = new LoggingConfiguration(logger, filter ?? DefaultFilter);
            }
        }

        private static bool DefaultFilter(string category, LogLevel level)
        {
            return true;
        }

        private class Logger : ILogger
        {
            private readonly string categoryName;

            private readonly SqlLoggingProvider provider;

            public Logger(string categoryName, SqlLoggingProvider provider)
            {
                this.provider = provider;
                this.categoryName = categoryName;
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }

            public bool IsEnabled(LogLevel level)
            {
                return true;
            }

            public void Log<TState>(LogLevel level, EventId eventId, TState state, Exception exception,
                Func<TState, Exception, string> formatter)
            {
                var config = this.provider.configuration;

                if (config.Filter(this.categoryName, level))
                {
                    config.Logger(formatter(state, exception));
                }
            }
        }

        private class LoggingConfiguration
        {
            public readonly Func<string, LogLevel, bool> Filter;

            public readonly Action<string> Logger;

            public LoggingConfiguration(Action<string> logger, Func<string, LogLevel, bool> filter)
            {
                this.Logger = logger;
                this.Filter = filter;
            }
        }
    }
}