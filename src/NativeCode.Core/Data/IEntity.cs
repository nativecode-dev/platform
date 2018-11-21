namespace NativeCode.Core.Data
{
    using System;

    using JetBrains.Annotations;

    public interface IEntity
    {
        DateTimeOffset DateCreated { get; }

        [CanBeNull]
        DateTimeOffset? DateModified { get; }

        [CanBeNull]
        object KeyObject { get; }

        [CanBeNull]
        IEntityUser UserCreated { get; }

        [CanBeNull]
        IEntityUser UserModified { get; }
    }

    public interface IEntity<out T>
        where T : struct
    {
        T Key { get; }
    }
}
