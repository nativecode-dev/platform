namespace NativeCode.Clients
{
    using System.Collections.Generic;

    public class ResourcePage<T> : IResourcePage<T>
    {
        public ResourcePage(IEnumerable<T> results)
        {
            this.Results = results;
        }

        public IEnumerable<T> Results { get; }
    }
}
