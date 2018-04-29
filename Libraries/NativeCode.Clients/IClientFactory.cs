namespace NativeCode.Clients
{
    using System;

    public interface IClientFactory<out T>
        where T : IClient
    {
        T CreateClient(Uri baseUrl);
    }
}
