namespace NativeCode.Core.Data
{
    using System;

    using JetBrains.Annotations;

    public class EntityIdentifier
    {
        private const string EntityTag = "@tag-id";

        public EntityIdentifier([NotNull] string key)
        {
            if (key.StartsWith(EntityTag) == false)
            {
                throw new InvalidOperationException($"Invalid entity key: {key}");
            }

            this.KeyString = key;
        }

        public EntityIdentifier([NotNull] IEntity entity)
        {
            this.KeyString = EntityKeyString(entity);
        }

        public string KeyString { get; }

        public static bool operator ==(EntityIdentifier left, EntityIdentifier right)
        {
            return string.Equals(left?.KeyString, right?.KeyString);
        }

        public static implicit operator string(EntityIdentifier instance)
        {
            return instance.KeyString;
        }

        public static implicit operator EntityIdentifier(string key)
        {
            return new EntityIdentifier(key);
        }

        public static bool operator !=(EntityIdentifier left, EntityIdentifier right)
        {
            return string.Equals(left?.KeyString, right?.KeyString) == false;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return this.Equals((EntityIdentifier)obj);
        }

        public override int GetHashCode()
        {
            return this.KeyString != null ? this.KeyString.GetHashCode() : 0;
        }

        protected bool Equals(EntityIdentifier other)
        {
            return string.Equals(this.KeyString, other.KeyString);
        }

        private static string EntityKeyString(IEntity entity)
        {
            if (entity.KeyObject == null)
            {
                return null;
            }

            return $"{EntityTag}:{entity.GetType().Name}:{entity.KeyObject}";
        }
    }
}
