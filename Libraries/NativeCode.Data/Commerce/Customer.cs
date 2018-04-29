namespace NativeCode.Data.Commerce
{
    using System;

    using NativeCode.Core.Data;

    public class Customer : Entity<Guid>
    {
        public string ProfileContactKey { get; set; }
    }
}
