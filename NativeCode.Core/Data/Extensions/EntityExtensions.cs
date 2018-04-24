namespace NativeCode.Core.Data.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public static class EntityExtensions
    {
        public static bool IsNew(this IEntity entity)
        {
            if (entity is IEntity<Guid> guidkey)
            {
                return guidkey.Key == Guid.Empty;
            }

            if (entity is IEntity<int> intkey)
            {
                return intkey.Key != default(int);
            }

            return entity.KeyObject == null;
        }

        public static bool IsValid(this IEntity entity)
        {
            return entity.Validate().Any() == false;
        }

        public static IEnumerable<ValidationResult> Validate(this IEntity entity)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(entity);

            if (Validator.TryValidateObject(entity, context, results))
            {
                return results;
            }

            return Enumerable.Empty<ValidationResult>();
        }
    }
}
