namespace NativeCode.Clients.Radarr.Responses.Commands
{
    using System.Runtime.Serialization;

    public abstract class CommandOptions
    {
        [IgnoreDataMember]
        public abstract CommandKind Command { get; }
    }
}
