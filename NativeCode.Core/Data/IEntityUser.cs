namespace NativeCode.Core.Data
{
    public interface IEntityUser : IEntity
    {
        string DisplayName { get; }

        string EntityIdentifier { get; }
    }
}
