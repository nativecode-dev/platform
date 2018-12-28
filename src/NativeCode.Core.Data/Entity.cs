namespace NativeCode.Core.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Diagnostics.CodeAnalysis;
    using System.Security.Principal;

    public abstract class Entity : IEntityAuditor
    {
        public DateTimeOffset DateCreated { get; protected set; }

        public DateTimeOffset? DateModified { get; protected set; }

        public string UserCreated { get; protected set; }

        public string UserModified { get; protected set; }

        void IEntityAuditor.SetDateCreated(DateTimeOffset value)
        {
            this.SetDateCreated(value);
        }

        void IEntityAuditor.SetDateModified(DateTimeOffset value)
        {
            this.SetDateModified(value);
        }

        void IEntityAuditor.SetUserCreated(IIdentity identity)
        {
            this.SetUserCreated(identity);
        }

        void IEntityAuditor.SetUserModified(IIdentity identity)
        {
            this.SetUserModified(identity);
        }

        protected void SetDateCreated(DateTimeOffset value)
        {
            this.DateCreated = value;
        }

        protected void SetDateModified(DateTimeOffset value)
        {
            this.DateModified = value;
        }

        protected void SetUserCreated(IIdentity identity)
        {
            this.UserCreated = identity.Name;
        }

        protected void SetUserModified(IIdentity identity)
        {
            this.UserModified = identity.Name;
        }
    }

    [SuppressMessage("Microsoft.StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass")]
    public abstract class Entity<TKey> : Entity, IEntity<TKey>
        where TKey : struct
    {
        [Key]
        public TKey Id { get; set; }
    }
}
