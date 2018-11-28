namespace NativeCode.Clients
{
    using System;

    public interface IClientFactory<out T>
        where T : ICommonClient
    {
        T CreateClient(Uri address);

        T CreateClient(Uri address, string apikey);
    }
}