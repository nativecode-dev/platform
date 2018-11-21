namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class Entity : IEntity
    {
        public DateTimeOffset DateCreated { get; } = DateTimeOffset.UtcNow;

        public DateTimeOffset? DateModified { get; set; } = default(DateTimeOffset);

        public object KeyObject { get; protected set; }

        public IEntityUser UserCreated { get; set; }

        public IEntityUser UserModified { get; set; }
    }

    public abstract class Entity<T> : Entity, IEntity<T>
        where T : struct
    {
        [Key]
        public T Key
        {
            get
            {
                var key = this.KeyObject;

                if (key != null)
                {
                    return (T)key;
                }

                return default(T);
            }
            set => this.KeyObject = value;
        }
    }
}
