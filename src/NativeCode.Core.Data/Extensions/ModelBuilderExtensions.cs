namespace NativeCode.Core.Data.Extensions
{
    using System;
    using System.Collections.Concurrent;
    using System.Linq.Expressions;
    using JetBrains.Annotations;
    using Microsoft.EntityFrameworkCore;

    public static class ModelBuilderExtensions
    {
        private static readonly ConcurrentDictionary<Type, LambdaExpression> ExpressionCache =
            new ConcurrentDictionary<Type, LambdaExpression>();

        private static readonly ConstantExpression NullValue = Expression.Constant(null);

        public static ModelBuilder DisableCascadeDelete([NotNull] this ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var fk in entity.GetForeignKeys())
                {
                    fk.DeleteBehavior = DeleteBehavior.Restrict;
                }
            }

            return modelBuilder;
        }

        public static ModelBuilder EnableSoftDelete([NotNull] this ModelBuilder modelBuilder)
        {
            var type = typeof(IEntitySoftDeletable);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (type.IsAssignableFrom(entity.ClrType) == false)
                {
                    continue;
                }

                foreach (var foreignKey in entity.GetForeignKeys())
                {
                    foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
                }

                modelBuilder.Entity(entity.ClrType)
                    .HasQueryFilter(GetSoftDeleteFilter(entity.ClrType));
            }

            return modelBuilder;
        }

        private static LambdaExpression GetSoftDeleteFilter(Type type)
        {
            if (ExpressionCache.ContainsKey(type))
            {
                return ExpressionCache[type];
            }

            var parameter = Expression.Parameter(type, "x");
            var property = Expression.Property(parameter, nameof(IEntitySoftDeletable.DateSoftDeleted));
            var condition = Expression.MakeBinary(ExpressionType.Equal, property, NullValue);
            var expression = Expression.Lambda(condition, parameter);

            return ExpressionCache.AddOrUpdate(type, k => expression, (k, v) => expression);
        }
    }
}