namespace NativeCode.Clients
{
    using System;
    using System.Net;
    using JetBrains.Annotations;

    public interface ICommonClient
    {
        Uri BaseAddress { get; }

        void SetBasicAuth(string username, string password);

        void SetCookieContainer([CanBeNull] CookieContainer container);
    }
}