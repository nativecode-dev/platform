namespace NativeCode.Core.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Security.Principal;
    using System.Threading;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;

    public static class DbContextExtensions
    {
        public static T EnableQueryLogging<T>([NotNull] this T context, Action<string> logger,
            Func<string, LogLevel, bool> filter)
            where T : DbContext
        {
            var provider = context.GetInfrastructure();
            var factory = provider.GetRequiredService<ILoggerFactory>();

            SqlLoggingProvider.CreateOrModifyLoggerForDbContext(context.GetType(), factory, logger, filter);

            return context;
        }

        public static IEnumerable<(EntityEntry, IEntityAuditor)> GetAuditors<T>([NotNull] this T context)
            where T : DbContext
        {
            foreach (var entry in context.ChangeTracker.Entries())
            {
                if (entry.Entity is IEntityAuditor auditor)
                {
                    yield return (entry, auditor);
                }
            }
        }

        public static void UpdateAuditEntities<T>([NotNull] this T context)
            where T : DbContext
        {
            context.UpdateAuditEntities(Thread.CurrentPrincipal.Identity);
        }

        [SuppressMessage("ReSharper", "SwitchStatementMissingSomeCases")]
        public static void UpdateAuditEntities<T>([NotNull] this T context, [NotNull] IIdentity user)
            where T : DbContext
        {
            context.UpdateAuditEntities(
                (entry, auditor) =>
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditor?.SetDateCreated(DateTimeOffset.UtcNow);
                            auditor?.SetUserCreated(user);
                            return;

                        case EntityState.Modified:
                            auditor?.SetDateModified(DateTimeOffset.UtcNow);
                            auditor?.SetUserModified(user);
                            return;

                        default:
                            return;
                    }
                });
        }

        public static void UpdateAuditEntities<T>([NotNull] this T context, [NotNull] Action<EntityEntry, IEntityAuditor> callback)
            where T : DbContext
        {
            foreach (var (entry, auditor) in GetAuditors(context))
            {
                callback(entry, auditor);
            }
        }

        public static IReadOnlyDictionary<EntityEntry, IReadOnlyCollection<ValidationResult>> ValidateChanges<T>(
            [NotNull] this T context, bool throws = false)
            where T : DbContext
        {
            var results = ValidateChanges(context.ChangeTracker);

            if (results.Any() && throws)
            {
                var exceptions = results.SelectMany(kvp =>
                    kvp.Value.Select(value => new ValidationException(value.ErrorMessage)));
                throw new AggregateException(exceptions);
            }

            return results;
        }

        public static IReadOnlyDictionary<EntityEntry, IReadOnlyCollection<ValidationResult>> ValidateChanges(
            [NotNull] this ChangeTracker tracker)
        {
            var dictionary = new Dictionary<EntityEntry, IReadOnlyCollection<ValidationResult>>();

            var serviceProvider = tracker.Context.GetService<IServiceProvider>();

            var items = new Dictionary<object, object>();
            var entries = tracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                var entity = entry.Entity;
                var context = new ValidationContext(entity, serviceProvider, items);
                var results = new List<ValidationResult>();

                if (Validator.TryValidateObject(entity, context, results, true) == false)
                {
                    var blah = results.Where(result => string.IsNullOrWhiteSpace(result.ErrorMessage) == false)
                        .ToList();

                    dictionary.Add(entry, blah);
                }
            }

            return dictionary;
        }
    }
}
