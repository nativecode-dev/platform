namespace NativeCode.Konfd.Data.Security
{
    using System;

    [Flags]
    public enum PermissionFlags
    {
        Deny = 1 << 0,

        AllowAccess = 1 << 1,

        AllowRead = 1 << 2,

        AllowWrite = 1 << 3
    }
}
