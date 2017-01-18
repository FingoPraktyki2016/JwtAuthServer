using System;

namespace LegnicaIT.DataAccess.Enums
{
    [Flags]
    public enum UserRole
    {
        None = 0,
        User = 1,
        Manager = 2,
        SuperAdmin = 4
    }
}
