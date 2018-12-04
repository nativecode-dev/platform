namespace NativeCode.Core.Data
{
    public enum ContextSeederStrategy
    {
        Default = 0,

        OnlyNewRecords = 1,

        OnlyWhenEmpty = Default,
    }
}
