namespace NativeCode.Clients
{
    using System.Collections.Generic;

    public interface IResourcePage<out T>
    {
        IEnumerable<T> Results { get; }
    }
}
