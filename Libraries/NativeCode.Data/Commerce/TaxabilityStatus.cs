namespace NativeCode.Data.Commerce
{
    using System;

    [Flags]
    public enum TaxabilityStatus
    {
        None = 1 << 0,

        RequiredByState = 1 << 1,

        ResellerOnly = 1 << 2,

        RetailOnly = 1 << 3
    }
}
