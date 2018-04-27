namespace NativeCode.Clients
{
    using System;
    using System.Net;

    using JetBrains.Annotations;

    public interface IClient
    {
        Uri BaseAddress { get; }

        void SetBasicAuth(string username, string password);

        void SetCookieContainer([CanBeNull] CookieContainer container);
    }
}
