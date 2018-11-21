namespace NativeCode.Data.Profiles
{
    using NativeCode.Core.Data;

    public class State : Entity<int>
    {
        public string Abbreviation { get; set; }

        public string Name { get; set; }
    }
}